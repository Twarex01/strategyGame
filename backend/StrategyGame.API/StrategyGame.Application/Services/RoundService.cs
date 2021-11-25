using Microsoft.EntityFrameworkCore;
using StrategyGame.Application.ServiceInterfaces;
using StrategyGame.Common.Enums;
using StrategyGame.Common.Stores;
using StrategyGame.Domain.Game;
using StrategyGame.Entities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StrategyGame.Application.Services
{
    public class RoundService : IRoundService
    {
        private readonly IEntityStore<Round> roundStore;
        private readonly IEntityStore<StrategyGameUser> strategyGameUserStore;
        private readonly IEntityStore<Scoreboard> scoreboardStore;
        private readonly IEntityStore<Battle> battleStore;
        private readonly IEntityStore<Gathering> gatheringStore;

        //TODO: config
        private const int ticksPerRound = 6;
        private const int autoGain = 50;
        private const int stealPow = 4;
        private const int lossPow = 2;

        public RoundService(IEntityStore<Round> roundStore, IEntityStore<StrategyGameUser> strategyGameUserStore, IEntityStore<Scoreboard> scoreboardStore, IEntityStore<Battle> battleStore, IEntityStore<Gathering> gatheringStore)
        {
            this.roundStore = roundStore;
            this.strategyGameUserStore = strategyGameUserStore;
            this.scoreboardStore = scoreboardStore;
            this.battleStore = battleStore;
            this.gatheringStore = gatheringStore;
        }

        private async Task TickLeaderboard(CancellationToken cancellationToken) 
        {
            var users = await strategyGameUserStore.GetQuery(false)
                .Include(x => x.Resources)
                .ThenInclude(x => x.ResourceData)
                .Include(x => x.Buildings)
                .ThenInclude(x => x.BuildingData)
                .ToListAsync();

            foreach (var user in users)
            {
                int userScore = 0;

                var resources = users.SelectMany(x => x.Resources);

                foreach (var resource in resources)
                {
                    userScore += resource.Amount * resource.ResourceData.Value;
                }

                var buildings = users.SelectMany(x => x.Buildings);

                foreach (var building in buildings)
                {
                    userScore += building.Amount * building.BuildingData.Value;
                }

                var score = await scoreboardStore.GetQuery(true).SingleOrDefaultAsync(x => x.StrategyGameUserId == user.Id);

                score.Score = userScore;
            }

            await scoreboardStore.SaveChanges();
        }

        private async Task TickBattles(CancellationToken cancellationToken)
        {
            //try
            //{            
            //    //TODO: Fix
            //    var battles = battleStore.GetQuery(false);
            //    var users = strategyGameUserStore.GetQuery(true).Include(x => x.Resources).ThenInclude(x => x.ResourceData);

            //    foreach (var battle in battles)
            //    {
            //        if (battle.TicksLeft <= 0)
            //        {
            //            var defPlayer = users.SingleOrDefault(x => x.Id == battle.DefPlayer);
            //            var atkPlayer = users.SingleOrDefault(x => x.Id == battle.AtkPlayer);

            //            if (battle.AtkPower > defPlayer.Resources.SingleOrDefault(x => x.ResourceData.Type == ResourceType.Atk).Amount)
            //            {
            //                var defUnits = defPlayer.Resources.SingleOrDefault(x => x.ResourceData.Type == ResourceType.Atk);

            //                defUnits.Amount = defUnits.Amount - defUnits.Amount / lossPow;

            //                var defResources = defPlayer.Resources.Where(x => x.ResourceData.Type != ResourceType.Atk);

            //                foreach (var resource in defResources)
            //                {
            //                    var stolenAmount = resource.Amount - resource.Amount / stealPow;

            //                    atkPlayer.Resources.SingleOrDefault(x => x.ResourceData.Type == resource.ResourceData.Type).Amount += stolenAmount;
            //                    resource.Amount -= stolenAmount;
            //                }
            //            }
            //            else
            //            {
            //                battle.AtkPower = battle.AtkPower - battle.AtkPower / lossPow;
            //            }

            //            var atkUnits = atkPlayer.Resources.SingleOrDefault(x => x.ResourceData.Type == ResourceType.Atk).Amount += battle.AtkPower;

            //            battleStore.Remove(battle);

            //            //Notify
            //        }
            //        else
            //        {
            //            battle.TicksLeft--;
            //        }
            //    }

            //    await battleStore.SaveChanges();
            //    await strategyGameUserStore.SaveChanges();
            //}
            //catch (Exception e)
            //{

            //    throw;
            //}
        }

        private async Task TickGathering(CancellationToken cancellationToken)
        {
            try
            {
                var users = strategyGameUserStore.GetQuery(true)
                                 .Include(x => x.Resources)
                                 .ThenInclude(x => x.ResourceData)
                                 .Include(x => x.Gatherings)
                                 .ThenInclude(x => x.GatheringData);

                foreach (var user in users)
                {
                    foreach (var gathering in user.Gatherings)
                    {
                        if (gathering.TicksLeft <= 0)
                        {
                            user.Resources.SingleOrDefault(x => x.ResourceData.Type == gathering.GatheringData.Type).Amount += gathering.CalcualtedReward;

                            gatheringStore.Remove(gathering);
                        }
                        else
                        {
                            gathering.TicksLeft--;
                        }
                    }
                }

                await gatheringStore.SaveChanges();
                await strategyGameUserStore.SaveChanges();
            }
            catch (Exception e)
            {

                throw;
            }
        }


        private async Task TickResources(CancellationToken cancellationToken) 
        {
            var users = strategyGameUserStore.GetQuery(true)
                                             .Include(x => x.Resources)
                                             .ThenInclude(x => x.ResourceData)
                                             .Include(x => x.Buildings)
                                             .ThenInclude(x => x.BuildingData)
                                             .ThenInclude(x => x.FactoryParameters);

            foreach (var user in users)
            {
                foreach (var resource in user.Resources)
                {
                    var resourceGain = autoGain;

                    var resourceType = resource.ResourceData.Type;

                    var factories = user.Buildings.Where(x => x.BuildingData.Type == BuildingType.Factory && x.BuildingData.FactoryParameters.ResourceType == resourceType);

                    foreach (var factory in factories)
                    {
                        resourceGain += factory.Amount * factory.BuildingData.FactoryParameters.PassiveIncome;
                    }

                    resource.Amount += resourceGain;
                }
            }

            await strategyGameUserStore.SaveChanges();
        }

        public async Task TickRound(CancellationToken cancellationToken = default)
        {
            await TickBattles(cancellationToken);
            await TickGathering(cancellationToken);

            var round = await roundStore.SingleOrDefault(true, cancellationToken);

            if (round.TicksLeft <= 0)
            {
                await TickResources(cancellationToken);

                await TickLeaderboard(cancellationToken);

                round.TicksLeft = ticksPerRound;

                round.Current++;
            }
            else 
            {
                round.TicksLeft--;
            }

            await roundStore.SaveChanges(cancellationToken);
        }

        public async Task<int> GetRound(CancellationToken cancellationToken)
        {
            var round = await roundStore.SingleOrDefault(false, cancellationToken);

            return round.Current;
        }
    }
}
