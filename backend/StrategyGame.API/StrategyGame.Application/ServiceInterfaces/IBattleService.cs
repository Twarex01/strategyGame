using StrategyGame.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StrategyGame.Application.ServiceInterfaces
{
    public interface IBattleService
    {
        public Task LaunchAttack(AttackActionDto attackActionDto, CancellationToken cancellationToken);
    }
}
