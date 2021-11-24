using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StrategyGame.Domain.Game;
using StrategyGame.Entities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.Infrastructure.EntityConfigurations
{
    public class ScoreboardEntityConfiguration : IEntityTypeConfiguration<Scoreboard>
    {

        public void Configure(EntityTypeBuilder<Scoreboard> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
