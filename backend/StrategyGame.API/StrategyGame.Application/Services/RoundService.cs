using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StrategyGame.Application.Dtos;
using StrategyGame.Application.Hubs;
using StrategyGame.Application.Options;
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
        private readonly IHubContext<RoundHub, IRoundHubClient> hubContext;

        private readonly RoundOptions roundOptions;

        public RoundService(IEntityStore<Round> roundStore,
                            IEntityStore<StrategyGameUser> strategyGameUserStore,
                            IEntityStore<Scoreboard> scoreboardStore,
                            IEntityStore<Battle> battleStore,
                            IEntityStore<Gathering> gatheringStore,
                            IHubContext<RoundHub, IRoundHubClient> hubContext,
                            IOptionsSnapshot<RoundOptions> roundOptionsSnapshot)
        {
            this.roundStore = roundStore;
            this.strategyGameUserStore = strategyGameUserStore;
            this.scoreboardStore = scoreboardStore;
            this.battleStore = battleStore;
            this.gatheringStore = gatheringStore;
            this.hubContext = hubContext;
            this.roundOptions = roundOptionsSnapshot.Value;
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

                var resources = user.Resources;

                foreach (var resource in resources)
                {
                    userScore += resource.Amount * resource.ResourceData.Value;
                }

                var buildings = user.Buildings;

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
            try
            {
                var battles = battleStore.GetQuery(true)
                    .Include(x => x.AtkPlayer).ThenInclude(x => x.Resources).ThenInclude(x => x.ResourceData)
                    .Include(x => x.DefPlayer).ThenInclude(x => x.Resources).ThenInclude(x => x.ResourceData);

                foreach (var battle in battles)
                {
                    bool attackSuccesful = false;
                    int atkUnitsLost = 0;
                    int defUnitsLost = 0;

                    if (battle.TicksLeft <= 0)
                    {
                        var defPower = battle.DefPlayer.Resources.SingleOrDefault(x => x.ResourceData.Type == ResourceType.Atk);

                        if (battle.AtkPower > defPower.Amount)
                        {
                            attackSuccesful = true;

                            defUnitsLost = defPower.Amount / roundOptions.LossPow;

                            defPower.Amount = defPower.Amount - defUnitsLost;

                            var defResources = battle.DefPlayer.Resources.Where(x => x.ResourceData.Type != ResourceType.Atk);

                            foreach (var resource in defResources)
                            {
                                var stolenAmount = resource.Amount - resource.Amount / roundOptions.StealPow;

                                battle.AtkPlayer.Resources.SingleOrDefault(x => x.ResourceData.Type == resource.ResourceData.Type).Amount += stolenAmount;

                                resource.Amount -= stolenAmount;
                            }
                        }
                        else
                        {
                            atkUnitsLost = battle.AtkPower / roundOptions.LossPow;

                            battle.AtkPower = battle.AtkPower - atkUnitsLost;
                        }

                        var atkUnits = battle.AtkPlayer.Resources.SingleOrDefault(x => x.ResourceData.Type == ResourceType.Atk).Amount += battle.AtkPower;

                        battleStore.Remove(battle);

                        //TODO: set claim
                        await hubContext.Clients.All.AttackEnded(new BattleDoneDto { Success = attackSuccesful, UnitsLost = atkUnitsLost });
                        await hubContext.Clients.All.DefenseEnded(new BattleDoneDto { Success = !attackSuccesful, UnitsLost = defUnitsLost });
                    }
                    else
                    {
                        battle.TicksLeft--;
                    }
                }

                await battleStore.SaveChanges();
                await strategyGameUserStore.SaveChanges();
            }
            catch (Exception e)
            {

                throw;
            }
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

                            await hubContext.Clients.All.GatherDone();
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
                    var resourceGain = roundOptions.AutoGain;

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

                round.TicksLeft = roundOptions.TicksPerRound;

                round.Current++;

                await hubContext.Clients.All.TurnEnded();
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
