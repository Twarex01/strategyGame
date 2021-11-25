using StrategyGame.Common.Stores;
using StrategyGame.Entities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.Domain.Game
{
    public class Gathering : IEntity
    {
        public Guid Id { get; set; }

        public Guid StrategyGameUserId { get; set; }
        public StrategyGameUser StrategyGameUser { get; set; }

        public int TicksLeft { get; set; }

        public int CalcualtedReward { get; set; }

        public Guid GatheringDataId { get; set; }

        public GatheringData GatheringData { get; set; }
    }
}
