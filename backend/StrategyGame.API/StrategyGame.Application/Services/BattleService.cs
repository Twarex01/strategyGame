using Microsoft.EntityFrameworkCore;
using StrategyGame.Application.Dtos;
using StrategyGame.Application.ServiceInterfaces;
using StrategyGame.Common.Claims;
using StrategyGame.Common.Constants;
using StrategyGame.Common.Enums;
using StrategyGame.Common.Stores;
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
    public class BattleService : IBattleService
    {
        private readonly IEntityStore<StrategyGameUser> strategyGameUserStore;
        private readonly IEntityStore<Battle> battleStore;
        private readonly IClaimService claimService;

        //TODO: From config
        private const int attackTime = 3;

        public BattleService(IEntityStore<StrategyGameUser> strategyGameUserStore, IEntityStore<Battle> battleStore, IClaimService claimService)
        {
            this.strategyGameUserStore = strategyGameUserStore;
            this.battleStore = battleStore;
            this.claimService = claimService;
        }

        public async Task LaunchAttack(AttackActionDto attackActionDto, CancellationToken cancellationToken)
        {
            var allPlayers = strategyGameUserStore.GetQuery(true).Include(x => x.Resources).ThenInclude(x => x.ResourceData);

            var userId = claimService.GetUserId();

            if (allPlayers.Count() < 2)
                throw new Exception(ErrorMessages.NotEnoughPlayers);

            if (attackActionDto.Atk < 1)
                throw new Exception(ErrorMessages.NotEnoughResources);

            var attackingPlayer = allPlayers.SingleOrDefault(x => x.Id == userId);

            var availableAtkPower = attackingPlayer.Resources.SingleOrDefault(x => x.ResourceData.Type == ResourceType.Atk);

            if (availableAtkPower.Amount < attackActionDto.Atk)
                throw new Exception(ErrorMessages.NotEnoughResources);

            availableAtkPower.Amount -= attackActionDto.Atk;

            Random random = new Random();

            var otherPlayers = await allPlayers.Where(x => x.Id != userId).ToListAsync();
            int index = random.Next(otherPlayers.Count);

            var attackedPlayer = otherPlayers.ElementAt(index);

            battleStore.Add(new Battle { AtkPlayer = userId, DefPlayer = attackedPlayer.Id, AtkPower = attackActionDto.Atk, TimeLeft = attackTime });

            await strategyGameUserStore.SaveChanges();
            await battleStore.SaveChanges();
        }
    }
}
