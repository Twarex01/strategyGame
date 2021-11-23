using StrategyGame.Common.Entities;
using StrategyGame.Common.Enums;

namespace StrategyGame.Domain.Game
{
    public class BuildingPrice : EFKeyValuePair<ResourceType, int>
    {
        protected BuildingPrice()
        {
        }

        public BuildingPrice(ResourceType key, int value)
        {
            this.Key = key;
            this.Value = value;
        }
    }
}
