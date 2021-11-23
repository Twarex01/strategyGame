using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StrategyGame.Domain.Game;

namespace StrategyGame.Infrastructure.EntityConfigurations
{
    public class BuildingEntityConfiguration : IEntityTypeConfiguration<Building>
    {

        public void Configure(EntityTypeBuilder<Building> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.BuildingData).WithMany();
        }
    }
}
