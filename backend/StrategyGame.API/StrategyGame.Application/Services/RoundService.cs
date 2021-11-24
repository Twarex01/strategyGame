using Microsoft.EntityFrameworkCore;
using StrategyGame.Application.ServiceInterfaces;
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

        public RoundService(IEntityStore<Round> roundStore, IEntityStore<StrategyGameUser> strategyGameUserStore, IEntityStore<Scoreboard> scoreboardStore)
        {
            this.roundStore = roundStore;
            this.strategyGameUserStore = strategyGameUserStore;
            this.scoreboardStore = scoreboardStore;
        }

        private async Task CalculateLeaderboard() 
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

        public async Task EndRound(CancellationToken cancellationToken)
        {
            await CalculateLeaderboard();

            var round = await roundStore.SingleOrDefault(true, cancellationToken);

            round.Current++;

            await roundStore.SaveChanges(cancellationToken);
        }

        public async Task<int> GetRound(CancellationToken cancellationToken)
        {
            var round = await roundStore.SingleOrDefault(false, cancellationToken);

            return round.Current;
        }
    }
}
