using Microsoft.AspNetCore.Identity;
using StrategyGame.Common.Stores;
using StrategyGame.Domain.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.Entities.Domain
{
    public class StrategyGameUser : IdentityUser<Guid>, IEntity
    {
        public IEnumerable<Resource> Resources { get; set; } = default!;
        public IEnumerable<Building> Buildings { get; set; } = default!;
        public IEnumerable<Gathering> Gatherings { get; set; } = default!;
    }
}
