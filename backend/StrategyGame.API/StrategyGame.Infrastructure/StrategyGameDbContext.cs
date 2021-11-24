using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StrategyGame.Domain;
using StrategyGame.Domain.Game;
using StrategyGame.Entities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.Infrastructure
{
    public class StrategyGameDbContext : IdentityDbContext<
        StrategyGameUser, StrategyGameRole, Guid, IdentityUserClaim<Guid>, StrategyGameUserRole, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public DbSet<Resource> Resources { get; set; }        
        public DbSet<ResourceData> ResourceDatas { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<BuildingData> BuildingDatas { get; set; }
        public DbSet<Gathering> Gatherings { get; set; }
        public DbSet<GatheringData> GatheringDatas { get; set; }
        public DbSet<Battle> Battles { get; set; }
        public DbSet<Round> Round { get; set; }
        public DbSet<Scoreboard> Scoreboard { get; set; }

        public StrategyGameDbContext(DbContextOptions<StrategyGameDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
