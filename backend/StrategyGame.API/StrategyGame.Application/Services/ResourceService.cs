using StrategyGame.Application.ServiceInterfaces;
using StrategyGame.Application.ViewModels;
using StrategyGame.Common.Stores;
using StrategyGame.Domain;
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

        public ResourceService(IEntityStore<StrategyGameUser> strategyGameUserStore)
        {
            this.strategyGameUserStore = strategyGameUserStore;
        }

        public async Task<IEnumerable<ResourceViewModel>> GetAllResources(CancellationToken cancellationToken)
        {
            var result = strategyGameUserStore.GetQuery(false);

            throw new NotImplementedException();
        }
    }
}
