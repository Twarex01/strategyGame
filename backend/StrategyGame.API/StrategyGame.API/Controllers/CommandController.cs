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
        private readonly IBattleService battleService;

        public CommandController(ICommandService commandService, IBattleService battleService)
        {
            this.commandService = commandService;
            this.battleService = battleService;
        }

        [HttpGet("build/actions")]
        public async Task<IEnumerable<BuildingViewModel>> GetBuildings(CancellationToken cancellationToken)
        {
            return await commandService.QueryBuildings(cancellationToken);
        }

        [HttpPost("build")]
        public async Task PostBuild(BuildingActionDto buildingActionDto, CancellationToken cancellationToken)
        {
            await commandService.StartBuildingAction(buildingActionDto, cancellationToken);
        }

        [HttpGet("gather/actions")]
        public async Task<IEnumerable<GatheringViewModel>> GetActions(CancellationToken cancellationToken)
        {
            //TODO: NPC Battle
            return await commandService.QueryGatheringActions(cancellationToken);
        }

        [HttpPost("gather")]
        public async Task PostGather([FromBody] GatheringActionDto gatheringActionDto, CancellationToken cancellationToken)
        {
            await commandService.StartGatheringAction(gatheringActionDto, cancellationToken);
        }

        [HttpPost("attack")]
        public async Task PostAttack([FromBody] AttackActionDto attackActionDto, CancellationToken cancellationToken)
        {
            await battleService.LaunchAttack(attackActionDto, cancellationToken);
        }
    }
}
