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
    [Route("api/command")]
    [ApiController]
    public class CommandController : Controller
    {
        public CommandController()
        {
        }

        [HttpPost("build")]
        public Task PostBuild(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        [HttpPost("capture")]
        public Task PostCapture(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        [HttpPost("attack")]
        public Task PostAttack(CancellationToken cancellationToken)
        {
            //-> Nincs másik játékos?
            throw new NotImplementedException();
        }

        //TODO: Védekezés -> signalr?
    }
}
