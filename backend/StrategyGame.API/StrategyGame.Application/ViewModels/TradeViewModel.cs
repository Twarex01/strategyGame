using StrategyGame.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.Application.ViewModels
{
    public class TradeViewModel
    {
        public Guid Id { get; set; }

        public int RiskPercentage { get; set; }

        public int ReturnMultiplier { get; set; }

        public ResourceType RewardResource { get; set; }

        public ResourceType RequiredResource { get; set; }
    }
}
