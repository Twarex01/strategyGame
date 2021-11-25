using StrategyGame.Common.Stores;
using StrategyGame.Entities.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.Domain.Game
{
    public class Battle : IEntity
    {
        public Guid Id { get; set; }

        public int AtkPower { get; set; }

        public Guid AtkPlayerId { get; set; }

        public Guid DefPlayerId { get; set; }

        public virtual StrategyGameUser AtkPlayer { get; set; }

        public virtual StrategyGameUser DefPlayer { get; set; }

        public int TicksLeft { get; set; }
    }
}
