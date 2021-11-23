using Microsoft.AspNetCore.Identity;
using StrategyGame.Common.Constants;
using StrategyGame.Common.Entities;
using StrategyGame.Common.Enums;
using StrategyGame.Common.Stores;
using StrategyGame.Domain;
using StrategyGame.Domain.Game;
using StrategyGame.Entities.Domain;
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

        private readonly IEntityStore<Resource> resourceStore;

        private readonly IEntityStore<BuildingData> buildingDataStore;

        private readonly IEntityStore<Building> buildingStore;

        private const string UserId = "11111111-1111-1111-1111-111111111111";

        public Seeder(RoleManager<StrategyGameRole> roleManager, UserManager<StrategyGameUser> userManager, IEntityStore<Resource> resourceStore, IEntityStore<BuildingData> buildingDataStore, IEntityStore<Building> buildingStore)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.resourceStore = resourceStore;
            this.buildingDataStore = buildingDataStore;
            this.buildingStore = buildingStore;
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

        //TODO: On new user
        private async Task SeedPlayerResources(Guid userId) 
        {
            foreach (ResourceType resourceType in Enum.GetValues(typeof(ResourceType)))
            {
                resourceStore.Add(new Resource { Amount = 0, PlayerId = userId, Type = resourceType });
            }

            await resourceStore.SaveChanges();
        }

        //TODO: Foreach -> From config
        private async Task SeedBuildingData() 
        {
            var previousBuildingData = buildingDataStore.GetQuery(false);

            //temporary
            var type = BuildingType.Mill;
            if (previousBuildingData.Any(x => x.Type == type))
                return;

            buildingDataStore.Add(new BuildingData { Type = type, Cost = new List<BuildingPrice> { new BuildingPrice(ResourceType.Wood, 1) } });

            await buildingDataStore.SaveChanges();
        }

        private async Task SeedPlayerBuildings(Guid userId)
        {
            var allBuildingData = buildingDataStore.GetQuery(false);

            foreach (var buildingData in allBuildingData)
            {
                buildingStore.Add(new Building { Amount = 0, PlayerId = userId, BuildingDataId = buildingData.Id });
            }

            await buildingStore.SaveChanges();
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

            try
            {
                await SeedPlayerResources(user.Id);
                await SeedPlayerBuildings(user.Id);
            }
            catch (Exception)
            {
                await userManager.DeleteAsync(user);

                throw;
            }
        }

        public async Task SeedGame()
        {
            await SeedBuildingData();

            await SeedRoles();
            await SeedUsers();
        }
    }
}
