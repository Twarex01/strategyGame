using StrategyGame.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StrategyGame.Application.ServiceInterfaces
{
    public interface IStatsService
    {
        public Task<IEnumerable<ScoreboardViewModel>> GetScoreboard(CancellationToken cancellationToken);
        public Task<int> GetScore(CancellationToken cancellationToken);
    }
}
