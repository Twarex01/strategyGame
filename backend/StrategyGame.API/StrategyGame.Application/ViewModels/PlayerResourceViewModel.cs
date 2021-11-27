using StrategyGame.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.Application.ViewModels
{
    public class PlayerResourceViewModel
    {
        public ResourceType Type { get; set; }

        public int Amount { get; set; }
    }
}
