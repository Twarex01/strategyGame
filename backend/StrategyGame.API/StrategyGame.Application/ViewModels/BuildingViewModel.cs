using StrategyGame.Common.Enums;
using StrategyGame.Domain.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.Application.ViewModels
{
    public class BuildingViewModel
    {
        public Guid Id { get; set; }

        public BuildingType BuildingType { get; set; }

        public FactoryParameter? FactoryParameters { get; set; }

        public IEnumerable<BuildingPriceViewModel> BuildingPrice { get; set; }
    }
}
