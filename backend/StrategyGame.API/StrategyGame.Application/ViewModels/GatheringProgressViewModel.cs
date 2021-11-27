using StrategyGame.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.Application.ViewModels
{
    public class GatheringProgressViewModel
    {
        public int TimeLeft { get; set; }

        public ResourceType resourceType { get; set; }
    }
}
