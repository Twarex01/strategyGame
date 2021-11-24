using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StrategyGame.Domain.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.Infrastructure.EntityConfigurations
{
    public class TradeDataEntityConfiguration : IEntityTypeConfiguration<TradeData>
    {

        public void Configure(EntityTypeBuilder<TradeData> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
