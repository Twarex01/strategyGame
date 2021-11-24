using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StrategyGame.Domain.Game;

namespace StrategyGame.Infrastructure.EntityConfigurations
{
    public class FactoryParametersEntityConfiguration : IEntityTypeConfiguration<FactoryParameters>
    {

        public void Configure(EntityTypeBuilder<FactoryParameters> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
