using StrategyGame.Application.ServiceInterfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Idea.Features.ProjektMicroservice.Application.Jobs
{
    public class RoundTickJob
    {
        private readonly IRoundService roundService;

        public RoundTickJob(IRoundService roundService)
        {
            this.roundService = roundService;
        }

        public async Task RunTick() 
        {
            await roundService.TickRound();

            //TODO: Signalr
        }
    }
}
