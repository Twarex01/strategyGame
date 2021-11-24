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
    public class ResourceDataEntityConfiguration : IEntityTypeConfiguration<ResourceData>
    {

        public void Configure(EntityTypeBuilder<ResourceData> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
