using StrategyGame.Common.Enums;
using StrategyGame.Common.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.Domain.Game
{
    public class FactoryParameters : IEntity
    {
        public Guid Id { get; set; }
        public int PassiveIncome { get; set; }
        public ResourceType ResourceType { get; set; }
    }
}
