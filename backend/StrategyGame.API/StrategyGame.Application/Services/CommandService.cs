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

        private readonly IClaimService claimService;

        public CommandService(IEntityStore<Gathering> gatheringStore, IEntityStore<GatheringData> gatheringDataStore, IClaimService claimService)
        {
            this.gatheringStore = gatheringStore;
            this.gatheringDataStore = gatheringDataStore;
            this.claimService = claimService;
        }

        public async Task<IEnumerable<GatheringViewModel>> QueryGatheringActions(CancellationToken cancellationToken)
        {
            return gatheringDataStore.GetQuery(false)
                                     .Select(x => new GatheringViewModel { 
                                         Id = x.Id, 
                                         MinimumBaseReward = x.MinimumBaseReward,
                                         MaximumBaseReward = x.MaximumBaseReward,
                                         MaxTimeAllowed = x.MaxTimeAllowed, 
                                         TimeMultiplier = x.TimeMultiplier, 
                                         Type = x.Type });
        }

        public async Task StartGatheringAction(GatheringActionDto gatheringActionDto, CancellationToken cancellationToken)
        {
            var userId = claimService.GetUserId();

            if (gatheringStore.GetQuery(false).Any(x => x.StrategyGameUserId == userId && x.TimeLeft > 0))
                throw new Exception(ErrorMessages.ActionInProgress);

            var action = await gatheringDataStore.GetEntity(gatheringActionDto.ActionId, false, cancellationToken);

            if (action.MaxTimeAllowed < gatheringActionDto.Time)
                throw new Exception(ErrorMessages.ActionRequestTooLong);

            //TODO: Effects? -> If any 
            var min = action.MinimumBaseReward * action.TimeMultiplier;
            var max = action.MaximumBaseReward * action.TimeMultiplier;

            Random random = new Random();
            var calculatedReward = random.Next(min, max);

            gatheringStore.Add(new Gathering { GatheringDataId = gatheringActionDto.ActionId, CalcualtedReward = calculatedReward, StrategyGameUserId = userId, TimeLeft = gatheringActionDto.Time });

            await gatheringStore.SaveChanges();
        }
    }
}
