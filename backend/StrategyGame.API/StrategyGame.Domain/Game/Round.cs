using StrategyGame.Common.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.Domain.Game
{
    public class Round : IEntity
    {
        public Guid Id { get; set; }
        public int Current { get; set; }
    }
}
