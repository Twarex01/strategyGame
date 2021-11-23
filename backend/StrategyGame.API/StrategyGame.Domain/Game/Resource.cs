using StrategyGame.Common.Enums;
using StrategyGame.Common.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.Domain.Game
{
    public class Resource : IEntity
    {
        public Guid Id { get; set; }

        public Guid PlayerId { get; set; }

        public int Amount { get; set; }

        public ResourceType Type { get; set; }
    }
}
