using StrategyGame.Common.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.Domain.Game
{
    public class Battle : IEntity
    {
        public Guid Id { get; set; }

        public int AtkPower { get; set; }

        public Guid AtkPlayer { get; set; }

        public Guid DefPlayer { get; set; }

        public int TimeLeft { get; set; }
    }
}
