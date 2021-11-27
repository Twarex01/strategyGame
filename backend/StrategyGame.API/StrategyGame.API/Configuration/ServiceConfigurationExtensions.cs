using StrategyGame.Application.ServiceInterfaces;
using StrategyGame.Application.Services;
using StrategyGame.Common.Claims;
using StrategyGame.Seeder;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceConfigurationExtensions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IClaimService, ClaimService>();

            services.AddScoped<IBattleService, BattleService>();
            services.AddScoped<ICommandService, CommandService>();
            services.AddScoped<IResourceService, ResourceService>();
            services.AddScoped<IBuildingService, BuildingService>();
            services.AddScoped<IRoundService, RoundService>();
            services.AddScoped<IStatsService, StatsService>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<ISeeder, Seeder>();

            return services;
        }
    }
}