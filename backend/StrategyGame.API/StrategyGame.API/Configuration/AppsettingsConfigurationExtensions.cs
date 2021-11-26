
using Microsoft.Extensions.Configuration;
using StrategyGame.Application.Options;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AppsettingsConfigurationExtensions
    {
        public static IServiceCollection ConfigureAppsettings(
               this IServiceCollection services,
               IConfiguration configuration)
        {
            services.Configure<JwtTokenOptions>(configuration.GetSection("JWT"));
            services.Configure<RoundOptions>(configuration.GetSection("Round"));
            services.Configure<BattleOptions>(configuration.GetSection("Battle"));

            return services;
        }
    }
}
