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

        private readonly IEntityStore<ResourceData> resourceDataStore;

        private readonly IEntityStore<BuildingData> buildingDataStore;

        private readonly IEntityStore<Building> buildingStore;

        private readonly IEntityStore<GatheringData> gatheringDataStore;

        private readonly IEntityStore<Gathering> gatheringStore;

        private readonly IEntityStore<Round> roundStore;

        private const string UserId = "11111111-1111-1111-1111-111111111111";

        public Seeder(RoleManager<StrategyGameRole> roleManager, UserManager<StrategyGameUser> userManager, IEntityStore<Resource> resourceStore, IEntityStore<ResourceData> resourceDataStore, IEntityStore<BuildingData> buildingDataStore, IEntityStore<Building> buildingStore, IEntityStore<GatheringData> gatheringDataStore, IEntityStore<Gathering> gatheringStore, IEntityStore<Round> roundStore)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.resourceStore = resourceStore;
            this.resourceDataStore = resourceDataStore;
            this.buildingDataStore = buildingDataStore;
            this.buildingStore = buildingStore;
            this.gatheringDataStore = gatheringDataStore;
            this.gatheringStore = gatheringStore;
            this.roundStore = roundStore;
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

        private async Task SeedResourceData()
        {
            var previousResourceData = resourceDataStore.GetQuery(false);

            foreach (ResourceType resourceType in Enum.GetValues(typeof(ResourceType)))
            {
                if (!previousResourceData.Any(x => x.Type == resourceType))
                    resourceDataStore.Add(new ResourceData { Value = 1, Type = resourceType });
            }

            await resourceDataStore.SaveChanges();
        }

        //TODO: On new user
        private async Task SeedPlayerResources(Guid userId) 
        {
            var allResourceData = resourceDataStore.GetQuery(false);

            foreach (var resourceData in allResourceData)
            {
                resourceStore.Add(new Resource { Amount = 0, ResourceDataId = resourceData.Id, StrategyGameUserId = userId });
            }

            await resourceStore.SaveChanges();
        }

        //TODO: From config
        private async Task SeedGatheringData()
        {
            var previousGatheringData = gatheringDataStore.GetQuery(false);

            foreach (ResourceType resourceType in Enum.GetValues(typeof(ResourceType)))
            {
                if(!previousGatheringData.Any(x => x.Type == resourceType))
                    gatheringDataStore.Add(new GatheringData { MinimumBaseReward = 1, MaximumBaseReward = 10, TimeMultiplier = 1, MaxTimeAllowed = 5 , Type = resourceType });
            }

            await gatheringDataStore.SaveChanges();
        }

        //TODO: Foreach -> From config
        private async Task SeedBuildingData() 
        {
            var previousBuildingData = buildingDataStore.GetQuery(false);

            //temporary
            var type = BuildingType.Factory;
            if (previousBuildingData.Count() == 3)
                return;

            buildingDataStore.Add(new BuildingData { Type = type, Cost = new List<BuildingPrice> { new BuildingPrice(ResourceType.Wood, 50) }, FactoryParameters = new FactoryParameters { PassiveIncome = 5, ResourceType = ResourceType.Wood } });
            buildingDataStore.Add(new BuildingData { Type = type, Cost = new List<BuildingPrice> { new BuildingPrice(ResourceType.Iron, 50) }, FactoryParameters = new FactoryParameters { PassiveIncome = 5, ResourceType = ResourceType.Iron } });
            buildingDataStore.Add(new BuildingData { Type = type, Cost = new List<BuildingPrice> { new BuildingPrice(ResourceType.Gold, 50) }, FactoryParameters = new FactoryParameters { PassiveIncome = 5, ResourceType = ResourceType.Gold } });

            await buildingDataStore.SaveChanges();
        }

        private async Task SeedPlayerBuildings(Guid userId)
        {
            var allBuildingData = buildingDataStore.GetQuery(false);

            foreach (var buildingData in allBuildingData)
            {
                buildingStore.Add(new Building { Amount = 0, StrategyGameUserId = userId, BuildingDataId = buildingData.Id });
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

        private async Task SeedRound() 
        {
            if (roundStore.GetQuery(false).Count() != 0)
                return;

            roundStore.Add(new Round { Current = 1 });
            await roundStore.SaveChanges();
        }

        public async Task SeedGame()
        {
            await SeedRound();

            await SeedResourceData();
            await SeedBuildingData();
            await SeedGatheringData();

            await SeedRoles();
            await SeedUsers();
        }
    }
}
