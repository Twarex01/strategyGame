using Microsoft.AspNetCore.Identity;
using StrategyGame.Common.Constants;
using StrategyGame.Common.Enums;
using StrategyGame.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.Seeder
{
    public class Seeder : ISeeder
    {
        private readonly RoleManager<StrategyGameRole> roleManager;

        private readonly UserManager<StrategyGameUser> userManager;

        private const string UserId = "11111111-1111-1111-1111-111111111111";

        public Seeder(RoleManager<StrategyGameRole> roleManager, UserManager<StrategyGameUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        private async Task SeedRoles()
        {
            foreach (var roleName in Enum.GetNames(typeof(Role)))
            {
                if ((await roleManager.FindByNameAsync(roleName)) == null)
                {
                    await roleManager.CreateAsync(new StrategyGameRole
                    {
                        Name = roleName
                    });
                }
            }
        }

        private async Task SeedUsers()
        {
            var userEntity = await userManager.FindByIdAsync(UserId);
            if (userEntity != null) 
                return;

            var user = new StrategyGameUser()
            {
                Id = Guid.Parse(UserId),
                UserName = "TestUser",
                Email = "TestUser@asd.com",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            await userManager.CreateAsync(user, "NicePassword");
            await userManager.AddClaimAsync(user, new Claim(Claims.UserId, user.Id.ToString()));
            await userManager.AddToRoleAsync(user, Role.User.ToString());
        }

        public async Task SeedUsersWithRoles()
        {
            await SeedRoles();
            await SeedUsers();
        }
    }
}
