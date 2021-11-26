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
        public Task AttackEnded(bool attackSuccesful, int unitsLost);
        public Task DefenseEnded(bool defenseSuccesful, int unitsLost);
        public Task GatherDone();
    }
}
