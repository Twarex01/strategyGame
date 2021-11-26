using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StrategyGame.Application.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StrategyGame.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            Configuration = configuration;
            HostEnvironment = hostEnvironment;
        }

        private IConfiguration Configuration { get; }

        private IWebHostEnvironment HostEnvironment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();

            services.ConfigureStrategyGameContext(Configuration, HostEnvironment.IsDevelopment());

            services.ConfigureAppsettings(Configuration);

            services.AddControllers();

            services.ConfigureAuthentication(Configuration);

            services.ConfigureStores();

            services.ConfigureServices();

            services.ConfigureHangfire(Configuration, HostEnvironment);

            services.ConfigureSignalR(Configuration, HostEnvironment);

            services.ConfigureIdentity();

            services.ConfigureSwagger();
        }

        public void Configure(IApplicationBuilder app, IRecurringJobManager jobManager)
        {
            if (HostEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwaggerWithUI();
            }

            app.UseHttpsRedirection();

            app.UseCors();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseHangfireJobs(jobManager, Configuration);

            app.UseHangfireDashboard();

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
                endpoints.MapControllers();
                endpoints.MapHub<RoundHub>("/roundhub");
            });
        }
    }
}
