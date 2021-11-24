using StrategyGame.Common.Enums;
using StrategyGame.Common.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.Domain.Game
{
    public class GatheringData : IEntity
    {
        public Guid Id { get; set; }

        public int MinimumBaseReward { get; set; }

        public int MaximumBaseReward { get; set; }

        public int TimeMultiplier { get; set; }

        public int MaxTimeAllowed { get; set; }

        public ResourceType Type { get; set; }
    }
}
