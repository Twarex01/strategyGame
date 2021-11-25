using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StrategyGame.Application.ServiceInterfaces
{
    public interface IRoundService
    {
        public Task TickRound(CancellationToken cancellationToken = default);
        public Task<int> GetRound(CancellationToken cancellationToken);
    }
}
