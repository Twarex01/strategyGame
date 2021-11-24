using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StrategyGame.Domain.Game;

namespace StrategyGame.Infrastructure.EntityConfigurations
{
    class GatheringDataEntityConfiguration : IEntityTypeConfiguration<GatheringData>
    {

        public void Configure(EntityTypeBuilder<GatheringData> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
