using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StrategyGame.Domain.Game;

namespace StrategyGame.Infrastructure.EntityConfigurations
{
    public class BuildingDataEntityConfiguration : IEntityTypeConfiguration<BuildingData>
    {

        public void Configure(EntityTypeBuilder<BuildingData> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
