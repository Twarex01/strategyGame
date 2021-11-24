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

        public RoundService(IEntityStore<Round> roundStore, IEntityStore<StrategyGameUser> strategyGameUserStore)
        {
            this.roundStore = roundStore;
            this.strategyGameUserStore = strategyGameUserStore;
        }

        private async Task CalculateLeaderboard() 
        {
            var users = strategyGameUserStore.GetQuery(false);

            var resources = await users.Select(x => x.Resources).ToListAsync();
            var buildings = await users.Select(x => x.Buildings).ToListAsync();
        }

        public async Task EndRound(CancellationToken cancellationToken)
        {
            await CalculateLeaderboard();

            var round = await roundStore.SingleOrDefault(true, cancellationToken);
        }

        public async Task<int> GetRound(CancellationToken cancellationToken)
        {
            var round = await roundStore.SingleOrDefault(false, cancellationToken);

            return round.Current;
        }
    }
}
