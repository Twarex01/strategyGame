using StrategyGame.Application.ServiceInterfaces;
using StrategyGame.Application.ViewModels;
using StrategyGame.Common.Claims;
using StrategyGame.Common.Stores;
using StrategyGame.Domain;
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
        private readonly IEntityStore<StrategyGameUser> strategyGameUserStore;
        private readonly IClaimService claimService;

        public ResourceService(IEntityStore<StrategyGameUser> strategyGameUserStore, IClaimService claimService)
        {
            this.strategyGameUserStore = strategyGameUserStore;
            this.claimService = claimService;
        }

        public async Task<IEnumerable<ResourceViewModel>> GetAllResources(CancellationToken cancellationToken)
        {
            var result = await strategyGameUserStore.GetEntity(claimService.GetUserId(), false, cancellationToken);

            throw new NotImplementedException();
        }
    }
}
