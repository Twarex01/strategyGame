using StrategyGame.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.Application.ViewModels
{
    public class TradeResultViewModel
    {
        public bool Success { get; set; }
        public int ResourcesWon { get; set; }
        public ResourceType WonType { get; set; }
        public int ResourcesLost { get; set; }
        public ResourceType LostType { get; set; }
    }
}
