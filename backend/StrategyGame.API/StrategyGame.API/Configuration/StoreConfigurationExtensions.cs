using Microsoft.AspNetCore.Mvc;
using StrategyGame.Application.Stores;
using StrategyGame.Common.Stores;
using StrategyGame.Domain;
using StrategyGame.Domain.Game;
using StrategyGame.Entities.Domain;
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

            services.AddScoped<IEntityStore<Resource>, ResourceStore>();
            services.AddScoped<IEntityStore<ResourceData>, ResourceDataStore>();
            services.AddScoped<IEntityStore<Building>, BuildingStore>();
            services.AddScoped<IEntityStore<BuildingData>, BuildingDataStore>();
            services.AddScoped<IEntityStore<Gathering>, GatheringStore>();
            services.AddScoped<IEntityStore<GatheringData>, GatheringDataStore>();

            services.AddScoped<IEntityStore<Round>, RoundStore>();
            services.AddScoped<IEntityStore<Scoreboard>, ScoreboardStore>();

            return services;
        }
    }
}

