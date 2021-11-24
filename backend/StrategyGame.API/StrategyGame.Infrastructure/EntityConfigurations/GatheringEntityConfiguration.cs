using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StrategyGame.Domain.Game;

namespace StrategyGame.Infrastructure.EntityConfigurations
{
    class GatheringEntityConfiguration : IEntityTypeConfiguration<Gathering>
    {

        public void Configure(EntityTypeBuilder<Gathering> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.GatheringData).WithMany();
        }
    }
}
