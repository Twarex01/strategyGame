using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StrategyGame.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.Infrastructure
{
    public class StrategyGameDbContext : IdentityDbContext<
        StrategyGameUser, StrategyGameRole, Guid, IdentityUserClaim<Guid>, StrategyGameUserRole, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public StrategyGameDbContext(DbContextOptions<StrategyGameDbContext> options) : base(options)
        {
        }
    }
}
