using Hangfire;
using Hangfire.SqlServer;
using Idea.Features.ProjektMicroservice.Application.Jobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SignalRConfigurationExtensions
    {
        public static IServiceCollection ConfigureSignalR(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            services.AddSignalR();

            return services;
        }
    }
}
