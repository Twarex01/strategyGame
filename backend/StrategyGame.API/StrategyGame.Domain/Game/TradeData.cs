using StrategyGame.Common.Enums;
using StrategyGame.Common.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.Domain.Game
{
    public class TradeData : IEntity
    {
        public Guid Id { get; set; }

        public int RiskPercentage { get; set; }

        public int ReturnMultiplier { get; set; }

        public ResourceType RewardResource { get; set; }

        public ResourceType RequiredResource { get; set; }
    }
}
