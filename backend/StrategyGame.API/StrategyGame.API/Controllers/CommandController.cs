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
        private readonly ICommandService commandService;

        public CommandController(ICommandService commandService)
        {
            this.commandService = commandService;
        }

        [HttpPost("build")]
        public Task PostBuild(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        [HttpGet("build/buildings")]
        public Task GetBuildings(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        [HttpPost("gather")]
        public async Task PostGather([FromBody] GatheringActionDto gatheringActionDto, CancellationToken cancellationToken)
        {
            await commandService.StartGatheringAction(gatheringActionDto, cancellationToken);
        }

        [HttpGet("gather/actions")]
        public async Task<IEnumerable<GatheringViewModel>> GetActions(CancellationToken cancellationToken)
        {
            return await commandService.QueryGatheringActions(cancellationToken);
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
