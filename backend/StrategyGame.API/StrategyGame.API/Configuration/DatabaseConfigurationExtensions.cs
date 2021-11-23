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
            //TODO: Different configuration extensions
            services.AddScoped<IBattleService, BattleService>();
            services.AddScoped<ICommandService, CommandService>();
            services.AddScoped<IResourceService, ResourceService>();
            services.AddScoped<IRoundService, RoundService>();
            services.AddScoped<IStatsService, StatsService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISeeder, Seeder>();

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
