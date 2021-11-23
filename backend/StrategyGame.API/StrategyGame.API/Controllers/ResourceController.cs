using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StrategyGame.Application.Dtos;
using StrategyGame.Application.ServiceInterfaces;
using StrategyGame.Application.ViewModels;
using StrategyGame.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StrategyGame.API.Controllers
{
    [Authorize]
    [Route("api/resource")]
    [ApiController]
    public class ResourceController : Controller
    {
        private readonly IResourceService resourceService;

        public ResourceController(IResourceService resourceService)
        {
            this.resourceService = resourceService;
        }

        [HttpGet("all")]
        public Task<IEnumerable<ResourceViewModel>> GetResources(CancellationToken cancellationToken)
        {
            return resourceService.GetAllResources(cancellationToken);
        }

        [HttpGet]
        public Task<IEnumerable<ResourceViewModel>> GetResource(ResourceType resource, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
