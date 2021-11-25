using Hangfire;
using Hangfire.SqlServer;
using Idea.Features.ProjektMicroservice.Application.Jobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class HangfireConfigurationExtensions
    {
        public static IServiceCollection ConfigureHangfire(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            services.AddHangfire(conf => conf
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseSqlServerStorage(configuration.GetConnectionString("HangfireConnection"),
                    new SqlServerStorageOptions
                    {
                        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                        QueuePollInterval = TimeSpan.Zero,
                        UseRecommendedIsolationLevel = true,
                        DisableGlobalLocks = true
                    }));

            services.AddHangfireServer();

            services.AddScoped<RoundTickJob>();

            return services;
        }

        public static IApplicationBuilder UseHangfireJobs(this IApplicationBuilder app, IRecurringJobManager jobManager, IConfiguration configuration)
        {
            string cron = $"*/{configuration[key: "Tick:IntervalInMinutes"]} * * * *";

            jobManager.AddOrUpdate<RoundTickJob>("tick", x => x.RunTick(), cron);

            return app;
        }
    }
}
