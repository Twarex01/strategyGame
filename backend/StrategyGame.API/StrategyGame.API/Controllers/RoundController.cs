using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StrategyGame.Application.Dtos;
using StrategyGame.Application.ServiceInterfaces;
using StrategyGame.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StrategyGame.API.Controllers
{
    [Authorize]
    [Route("api/round")]
    [ApiController]
    public class RoundController : Controller
    {
        private readonly IRoundService roundService;

        public RoundController(IRoundService roundService)
        {
            this.roundService = roundService;
        }

        [HttpGet]
        public async Task<int> GetRound(CancellationToken cancellationToken)
        {
            return await roundService.GetRound(cancellationToken);
        }

        //TODO: Until hangfire
        [HttpPost("end")]
        public async Task PostEndRound(CancellationToken cancellationToken)
        {
            await roundService.TickRound(cancellationToken);
        }
    }
}
