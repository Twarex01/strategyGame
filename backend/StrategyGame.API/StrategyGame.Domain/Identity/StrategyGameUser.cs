using Microsoft.AspNetCore.Identity;
using StrategyGame.Common.Stores;
using StrategyGame.Domain.Game;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.Entities.Domain
{
    public class StrategyGameUser : IdentityUser<Guid>, IEntity
    {
        public List<Resource> Resources { get; set; }
        public List<Building> Buildings { get; set; }
        public List<Gathering> Gatherings { get; set; }

        public virtual ICollection<Battle> AttackBattles { get; set; }
        public virtual ICollection<Battle> DefenseBattles { get; set; }
    }
}
