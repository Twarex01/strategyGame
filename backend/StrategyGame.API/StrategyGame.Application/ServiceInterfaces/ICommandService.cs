using StrategyGame.Application.Dtos;
using StrategyGame.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StrategyGame.Application.ServiceInterfaces
{
    public interface ICommandService
    {
        public Task StartGatheringAction(GatheringActionDto gatheringActionDto, CancellationToken cancellationToken);
        public Task StartBuildingAction(BuildingActionDto buildingActionDto, CancellationToken cancellationToken);
        public Task<TradeResultViewModel> StartTradeAction(TradeActionDto tradeActionDto, CancellationToken cancellationToken);
        public Task<IEnumerable<GatheringViewModel>> QueryGatheringActions(CancellationToken cancellationToken);
        public Task<GatheringProgressViewModel> QueryGatheringInProgress(CancellationToken cancellationToken);
        public Task<IEnumerable<BuildingViewModel>> QueryBuildingActions(CancellationToken cancellationToken);
        public Task<IEnumerable<TradeViewModel>> QueryTradeActions(CancellationToken cancellationToken);
    }
}
