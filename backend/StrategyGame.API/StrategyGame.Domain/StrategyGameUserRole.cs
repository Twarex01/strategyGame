using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.Domain
{
    public class StrategyGameUserRole : IdentityUserRole<Guid>
    {
        public StrategyGameUser HonvedUser { get; set; }

        public StrategyGameRole HonvedRole { get; set; }
    }
}
