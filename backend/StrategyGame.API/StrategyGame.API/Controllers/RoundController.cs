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
        public RoundController()
        {
        }

        [HttpGet]
        public Task<int> GetRound(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
