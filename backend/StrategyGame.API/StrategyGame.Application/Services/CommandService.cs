using Microsoft.EntityFrameworkCore;
using StrategyGame.Application.Dtos;
using StrategyGame.Application.ServiceInterfaces;
using StrategyGame.Application.ViewModels;
using StrategyGame.Common.Claims;
using StrategyGame.Common.Constants;
using StrategyGame.Common.Stores;
using StrategyGame.Domain.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StrategyGame.Application.Services
{
    public class CommandService : ICommandService
    {
        private readonly IEntityStore<Gathering> gatheringStore;
        private readonly IEntityStore<GatheringData> gatheringDataStore;
        private readonly IEntityStore<Building> buildingStore;
        private readonly IEntityStore<BuildingData> buildingDataStore;
        private readonly IEntityStore<TradeData> tradeDataStore;
        private readonly IEntityStore<Resource> resourceStore;

        private readonly IClaimService claimService;

        public CommandService(IEntityStore<Gathering> gatheringStore, IEntityStore<GatheringData> gatheringDataStore, IEntityStore<Building> buildingStore, IEntityStore<BuildingData> buildingDataStore, IEntityStore<TradeData> tradeDataStore, IEntityStore<Resource> resourceStore, IClaimService claimService)
        {
            this.gatheringStore = gatheringStore;
            this.gatheringDataStore = gatheringDataStore;
            this.buildingStore = buildingStore;
            this.buildingDataStore = buildingDataStore;
            this.tradeDataStore = tradeDataStore;
            this.resourceStore = resourceStore;
            this.claimService = claimService;
        }

        public async Task<IEnumerable<BuildingViewModel>> QueryBuildingActions(CancellationToken cancellationToken)
        {
            return buildingDataStore.GetQuery(false)
                                    .Include(x => x.Cost)
                                    .Select(x => new BuildingViewModel
                                    {
                                        Id = x.Id, BuildingType = x.Type, FactoryParameters = x.FactoryParameters, BuildingPrice = x.Cost.Select(y => new BuildingPriceViewModel { Key = y.Key, Value = y.Value })
                                    });
        }

        public async Task<IEnumerable<GatheringViewModel>> QueryGatheringActions(CancellationToken cancellationToken)
        {
            return gatheringDataStore.GetQuery(false)
                                     .Select(x => new GatheringViewModel 
                                     { 
                                         Id = x.Id, 
                                         MinimumBaseReward = x.MinimumBaseReward,
                                         MaximumBaseReward = x.MaximumBaseReward,
                                         MaxTimeAllowed = x.MaxTimeAllowed, 
                                         TimeMultiplier = x.TimeMultiplier, 
                                         Type = x.Type 
                                     });
        }

        public async Task<IEnumerable<TradeViewModel>> QueryTradeActions(CancellationToken cancellationToken)
        {
            return tradeDataStore.GetQuery(false)
                         .Select(x => new TradeViewModel
                         {
                             Id = x.Id,
                             RequiredResource = x.RequiredResource,
                             ReturnMultiplier = x.ReturnMultiplier,
                             RiskPercentage = x.RiskPercentage,
                             RewardResource = x.RewardResource
                         });
        }

        public async Task StartBuildingAction(BuildingActionDto buildingActionDto, CancellationToken cancellationToken)
        {
            //TODO: Concurency

            var userId = claimService.GetUserId();

            var buildingData = await buildingDataStore.GetEntity(buildingActionDto.BuildingId, false, cancellationToken);

            var playerResources = resourceStore.GetQuery(true).Include(x => x.ResourceData).Where(x => x.StrategyGameUserId == userId);

            foreach (var item in buildingData.Cost)
            {
                var resource = await playerResources.SingleOrDefaultAsync(x => x.ResourceData.Type == item.Key);

                if (item.Value > resource.Amount)
                    throw new Exception(ErrorMessages.NotEnoughResources);
            }

            foreach (var item in buildingData.Cost)
            {
                var resource = await playerResources.SingleOrDefaultAsync(x => x.ResourceData.Type == item.Key);

                resource.Amount -= item.Value;
            }

            buildingStore.GetQuery(true).Include(x => x.BuildingData).SingleOrDefault(x => x.BuildingDataId == buildingData.Id).Amount++;

            await resourceStore.SaveChanges();
            await buildingStore.SaveChanges();
        }

        public async Task StartGatheringAction(GatheringActionDto gatheringActionDto, CancellationToken cancellationToken)
        {
            var userId = claimService.GetUserId();

            if (gatheringStore.GetQuery(false).Any(x => x.StrategyGameUserId == userId && x.TicksLeft > 0))
                throw new Exception(ErrorMessages.ActionInProgress);

            var gather = await gatheringDataStore.GetEntity(gatheringActionDto.GatherId, false, cancellationToken);

            if (gather.MaxTimeAllowed < gatheringActionDto.Time)
                throw new Exception(ErrorMessages.ActionRequestTooLong);

            if (0 >= gatheringActionDto.Time)
                throw new Exception(ErrorMessages.ActionRequestTooShort);

            var min = gather.MinimumBaseReward * gather.TimeMultiplier * gatheringActionDto.Time;
            var max = gather.MaximumBaseReward * gather.TimeMultiplier * gatheringActionDto.Time;

            Random random = new Random();
            var calculatedReward = random.Next(min, max);

            gatheringStore.Add(new Gathering { GatheringDataId = gatheringActionDto.GatherId, CalcualtedReward = calculatedReward, StrategyGameUserId = userId, TicksLeft = gatheringActionDto.Time });

            await gatheringStore.SaveChanges();
        }

        public async Task<TradeResultViewModel> StartTradeAction(TradeActionDto tradeActionDto, CancellationToken cancellationToken)
        {
            var userId = claimService.GetUserId();

            var trade = await tradeDataStore.GetEntity(tradeActionDto.TradeId, false, cancellationToken);

            var query = resourceStore.GetQuery(true).Include(x => x.ResourceData).Where(x => x.StrategyGameUserId == userId);

            var gambledResource = query.SingleOrDefault(x => x.ResourceData.Type == trade.RequiredResource);
            var rewardResource = query.SingleOrDefault(x => x.ResourceData.Type == trade.RewardResource);

            if(gambledResource.Amount < tradeActionDto.Amount)
                throw new Exception(ErrorMessages.NotEnoughResources);

            Random random = new Random();

            if (random.Next(0, 100) > trade.RiskPercentage)
            {
                gambledResource.Amount -= tradeActionDto.Amount;

                await resourceStore.SaveChanges();

                return new TradeResultViewModel { Success = false, ResourcesWon = 0, WonType = trade.RewardResource, ResourcesLost = tradeActionDto.Amount, LostType = trade.RequiredResource };
            }
            else 
            {
                var reward = tradeActionDto.Amount * trade.ReturnMultiplier;

                rewardResource.Amount += reward;

                await resourceStore.SaveChanges();

                return new TradeResultViewModel { Success = false, ResourcesWon = reward, WonType = trade.RewardResource, ResourcesLost = 0, LostType = trade.RequiredResource };
            }
        }
    }
}
