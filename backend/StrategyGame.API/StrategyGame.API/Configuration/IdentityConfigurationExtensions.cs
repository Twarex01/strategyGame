using System;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StrategyGame.Domain;
using StrategyGame.Entities.Domain;
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
            var identityBuilder = AddIdentity(services);

            ConfigureIdentityStores(identityBuilder);
            //AddTokenProvider(identityBuilder);
        }

        private static IdentityBuilder AddIdentity(IServiceCollection services)
            => services.AddIdentity<StrategyGameUser, StrategyGameRole>(x =>
            {
                x.Password.RequiredLength = 6;
                x.Password.RequireNonAlphanumeric = false;
                x.Password.RequireDigit = false;
                x.Password.RequireUppercase = true;
                x.Password.RequireLowercase = true;
                x.User.RequireUniqueEmail = true;
            });

        public static void ConfigureIdentityStores(IdentityBuilder identityBuilder)
        {
            identityBuilder.AddRoleStore<RoleStore<StrategyGameRole, DbContext, Guid, StrategyGameUserRole, IdentityRoleClaim<Guid>>>();
            identityBuilder.AddUserStore<UserStore<StrategyGameUser, StrategyGameRole, DbContext, Guid, IdentityUserClaim<Guid>,
                StrategyGameUserRole, IdentityUserLogin<Guid>, IdentityUserToken<Guid>, IdentityRoleClaim<Guid>>>();
        }
    }
}
