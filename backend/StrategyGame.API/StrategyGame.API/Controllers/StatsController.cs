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
    [Route("api/stats")]
    [ApiController]
    public class StatsController : Controller
    {
        private readonly IStatsService statsService;

        public StatsController(IStatsService statsService)
        {
            this.statsService = statsService;
        }

        [HttpGet("score/all")]
        public Task<IEnumerable<ScoreboardViewModel>> GetScoreboard(CancellationToken cancellationToken)
        {
            return statsService.GetScoreboard(cancellationToken);
        }

        [HttpGet("score")]
        public Task<int> GetScore(CancellationToken cancellationToken)
        {
            return statsService.GetScore(cancellationToken);
        }
    }
}
