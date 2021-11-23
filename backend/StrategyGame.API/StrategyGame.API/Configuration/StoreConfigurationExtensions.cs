using Microsoft.AspNetCore.Mvc;
using StrategyGame.Application.Stores;
using StrategyGame.Common.Stores;
using StrategyGame.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class StoreConfigurationExtensions 
    {
        public static IServiceCollection ConfigureStores(this IServiceCollection services)
        {
            services.AddScoped<IEntityStore<StrategyGameUser>, StrategyGameUserStore>();

            return services;
        }
    }
}

