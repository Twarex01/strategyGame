using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StrategyGame.Application.ServiceInterfaces;
using StrategyGame.Application.Services;
using StrategyGame.Infrastructure;
using StrategyGame.Seeder;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DatabaseConfigurationExtensions
    {
        public static IServiceCollection ConfigureStrategyGameContext(
            this IServiceCollection services, IConfiguration Configuration, bool isDevelopment)
        {
            services.AddDbContext<DbContext, StrategyGameDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"),
                    optionsBuilder =>
                    {
                        optionsBuilder.MigrationsAssembly("StrategyGame.Infrastructure");
                    })
                    .EnableSensitiveDataLogging(isDevelopment)
                );

            return services;
        }
    }
}
