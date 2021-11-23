using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StrategyGame.Seeder;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Hosting
{
    public static class WebHostExtensions
    {
        public async static Task<IHost> SeedData(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var configuration = services.GetService<IConfiguration>();
                var seed = configuration.GetValue<bool>("Seed");

                var dbContext = services.GetRequiredService<DbContext>();

                dbContext.Database.Migrate();

                if (seed)
                {
                    var seeder = services.GetService<ISeeder>();

                    await seeder.SeedUsersWithRoles();
                }
            }

            return host;
        }
    }
}
