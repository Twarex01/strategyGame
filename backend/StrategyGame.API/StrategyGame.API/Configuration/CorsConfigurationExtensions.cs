using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class CorsConfigurationExtensions
    {
        public static IServiceCollection ConfigureCors(
            this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: "OriginsToAllow",
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                                  });
            });

            return services;
        }

        public static IApplicationBuilder UseConfiguredCors(this IApplicationBuilder app)
        {
            app.UseCors("OriginsToAllow");

            return app;
        }
    }
}
