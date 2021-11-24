using Microsoft.EntityFrameworkCore;
using StrategyGame.Application.ServiceInterfaces;
using StrategyGame.Application.ViewModels;
using StrategyGame.Common.Claims;
using StrategyGame.Common.Stores;
using StrategyGame.Domain.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StrategyGame.Application.Services
{
    public class StatsService : IStatsService
    {
        private readonly IEntityStore<Scoreboard> scoreboardEntityStore;
        private readonly IClaimService claimService;

        public StatsService(IEntityStore<Scoreboard> scoreboardEntityStore, IClaimService claimService)
        {
            this.scoreboardEntityStore = scoreboardEntityStore;
            this.claimService = claimService;
        }
        public async Task<IEnumerable<ScoreboardViewModel>> GetScoreboard(CancellationToken cancellationToken)
        {
            return scoreboardEntityStore.GetQuery(false)
                                        .Include(x => x.StrategyGameUser)
                                        .Select(x => new ScoreboardViewModel { PlayerEmail = x.StrategyGameUser.Email, Score = x.Score })
                                        .OrderByDescending(x => x.Score);
        }

        public async Task<int> GetScore(CancellationToken cancellationToken)
        {
            var userId = claimService.GetUserId();

            var scoreboard = await scoreboardEntityStore.GetQuery(false).SingleOrDefaultAsync(x => x.StrategyGameUserId == userId);

            return scoreboard.Score;
        }

    }
}
