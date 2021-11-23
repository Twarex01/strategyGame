using StrategyGame.Application.ServiceInterfaces;
using StrategyGame.Application.ViewModels;
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
        public IEnumerable<ResourceViewModel> GetAllResources(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
