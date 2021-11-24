using StrategyGame.Common.Enums;
using StrategyGame.Common.Stores;
using System;

namespace StrategyGame.Domain.Game
{
    public class ResourceData : IEntity
    {
        public Guid Id { get; set; }

        public int Value { get; set; }

        public ResourceType Type { get; set; }
    }
}
