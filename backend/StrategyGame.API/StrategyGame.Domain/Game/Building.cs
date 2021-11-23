using StrategyGame.Common.Enums;
using StrategyGame.Common.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.Domain.Game
{
    public class Building : IEntity
    {
        public Guid Id { get; set; }

        public Guid PlayerId { get; set; }

        public int Amount { get; set; }

        public Guid BuildingDataId { get; set; }

        public BuildingData BuildingData { get; set; }
    }
}
