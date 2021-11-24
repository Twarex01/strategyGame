using Microsoft.EntityFrameworkCore;
using StrategyGame.Application.ServiceInterfaces;
using StrategyGame.Application.ViewModels;
using StrategyGame.Common.Claims;
using StrategyGame.Common.Stores;
using StrategyGame.Domain;
using StrategyGame.Domain.Game;
using StrategyGame.Entities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StrategyGame.Application.Services
{
    public class ResourceService : IResourceService
    {
        private readonly IEntityStore<Resource> resourceStore;
        private readonly IClaimService claimService;

        public ResourceService(IEntityStore<Resource> resourceStore, IClaimService claimService)
        {
            this.resourceStore = resourceStore;
            this.claimService = claimService;
        }

        public async Task<IEnumerable<ResourceViewModel>> GetAllResources(CancellationToken cancellationToken)
        {
            return resourceStore.GetQuery(false)
                                .Include(x => x.ResourceData)
                                .Where(x => x.StrategyGameUserId == claimService.GetUserId())
                                .Select(x => new ResourceViewModel { Amount = x.Amount, Type = x.ResourceData.Type });
        }
    }
}
