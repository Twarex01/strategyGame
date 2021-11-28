using StrategyGame.Application.Dtos;
using StrategyGame.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.Application.Hubs
{
    public interface IRoundHubClient
    {
        public Task TurnEnded();
        public Task AttackEnded(BattleDoneDto battleDone);
        public Task DefenseEnded(BattleDoneDto battleDone);
        public Task GatherDone();
    }
}
