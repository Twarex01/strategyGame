using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StrategyGame.Domain.Game;

namespace StrategyGame.Infrastructure.EntityConfigurations
{
    public class FactoryParametersEntityConfiguration : IEntityTypeConfiguration<FactoryParameter>
    {

        public void Configure(EntityTypeBuilder<FactoryParameter> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
