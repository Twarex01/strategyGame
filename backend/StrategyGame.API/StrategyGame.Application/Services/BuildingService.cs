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
    public class BuildingService : IBuildingService
    {
        private readonly IEntityStore<Building> buildingStore;
        private readonly IClaimService claimService;

        public BuildingService(IEntityStore<Building> buildingStore, IClaimService claimService)
        {
            this.buildingStore = buildingStore;
            this.claimService = claimService;
        }

        public async Task<IEnumerable<PlayerBuildingViewModel>> GetAllBuildings(CancellationToken cancellationToken)
        {
            return buildingStore.GetQuery(false)
                                .Include(x => x.BuildingData)
                                .Where(x => x.StrategyGameUserId == claimService.GetUserId())
                                .Select(x => new PlayerBuildingViewModel { Amount = x.Amount, BuildingType = x.BuildingData.Type, FactoryParameters = x.BuildingData.FactoryParameters });
        }
    }
}
