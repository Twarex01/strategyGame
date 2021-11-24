using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StrategyGame.Entities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.Infrastructure.EntityConfigurations
{
    public class StrategyGameUserEntityConfiguration : IEntityTypeConfiguration<StrategyGameUser>
    {

        public void Configure(EntityTypeBuilder<StrategyGameUser> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Resources).WithOne(x => x.StrategyGameUser).HasForeignKey(x => x.StrategyGameUserId);

            builder.HasMany(x => x.Buildings).WithOne(x => x.StrategyGameUser).HasForeignKey(x => x.StrategyGameUserId);

            builder.HasMany(x => x.Gatherings).WithOne(x => x.StrategyGameUser).HasForeignKey(x => x.StrategyGameUserId);
        }
    }
}
