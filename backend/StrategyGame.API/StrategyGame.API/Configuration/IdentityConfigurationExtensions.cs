using System;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StrategyGame.Domain;
using StrategyGame.Infrastructure;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IdentityConfigurationExtensions
    {
        public static IServiceCollection ConfigureIdentity(
            this IServiceCollection services)
        {
            services.AddAuthentication();

            ConfigureIdentityServices(services);

            return services;
        }

        private static void ConfigureIdentityServices(IServiceCollection services)
        {
            services
                .AddIdentity<StrategyGameUser, StrategyGameRole>(x =>
                {
                    x.Password.RequiredLength = 6;
                    x.Password.RequireNonAlphanumeric = false;
                    x.Password.RequireDigit = false;
                    x.Password.RequireUppercase = true;
                    x.Password.RequireLowercase = true;
                    x.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<StrategyGameDbContext>();
        }
    }
}
