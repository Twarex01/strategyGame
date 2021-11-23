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
        public StatsController()
        {
        }

        [HttpGet("score/all")]
        public Task<IEnumerable<ScoreBoardViewModel>> GetScoreBoard(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        [HttpGet("score")]
        public Task<int> GetScore(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        [HttpGet("place")]
        public Task<int> GetPlace(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
