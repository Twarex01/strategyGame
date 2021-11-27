using StrategyGame.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StrategyGame.Application.ServiceInterfaces
{
    public interface IBuildingService
    {
        Task<IEnumerable<PlayerBuildingViewModel>> GetAllBuildings(CancellationToken cancellationToken);
    }
}
