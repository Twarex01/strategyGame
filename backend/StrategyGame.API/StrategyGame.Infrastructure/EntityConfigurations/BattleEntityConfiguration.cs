using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StrategyGame.Domain.Game;

namespace StrategyGame.Infrastructure.EntityConfigurations
{
    public class BattleEntityConfiguration : IEntityTypeConfiguration<Battle>
    {

        public void Configure(EntityTypeBuilder<Battle> builder)
        {
            builder.HasKey(x => x.Id);

            //TODO
        }
    }
}
