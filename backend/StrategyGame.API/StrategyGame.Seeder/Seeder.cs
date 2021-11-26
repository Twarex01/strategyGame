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

        private readonly IEntityStore<TradeData> tradeDataStore;

        private readonly IEntityStore<Round> roundStore;

        private readonly IEntityStore<Scoreboard> scoreboardStore;

        private const string User1Id = "11111111-1111-1111-1111-111111111111";
        private const string User2Id = "11111111-1111-1111-1111-111111111112";

        public Seeder(RoleManager<StrategyGameRole> roleManager, UserManager<StrategyGameUser> userManager, IEntityStore<Resource> resourceStore, IEntityStore<ResourceData> resourceDataStore, IEntityStore<BuildingData> buildingDataStore, IEntityStore<Building> buildingStore, IEntityStore<GatheringData> gatheringDataStore, IEntityStore<Gathering> gatheringStore, IEntityStore<TradeData> tradeDataStore, IEntityStore<Round> roundStore, IEntityStore<Scoreboard> scoreboardStore)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.resourceStore = resourceStore;
            this.resourceDataStore = resourceDataStore;
            this.buildingDataStore = buildingDataStore;
            this.buildingStore = buildingStore;
            this.gatheringDataStore = gatheringDataStore;
            this.gatheringStore = gatheringStore;
            this.tradeDataStore = tradeDataStore;
            this.roundStore = roundStore;
            this.scoreboardStore = scoreboardStore;
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

        private async Task SeedPlayerResources(Guid userId) 
        {
            var allResourceData = resourceDataStore.GetQuery(false);

            foreach (var resourceData in allResourceData)
            {
                resourceStore.Add(new Resource { Amount = 10, ResourceDataId = resourceData.Id, StrategyGameUserId = userId });
            }

            await resourceStore.SaveChanges();
        }

        private async Task SeedGatheringData()
        {
            var previousGatheringData = gatheringDataStore.GetQuery(false);

            foreach (ResourceType resourceType in Enum.GetValues(typeof(ResourceType)))
            {
                if (resourceType == ResourceType.Atk)
                    continue;

                if(!previousGatheringData.Any(x => x.Type == resourceType))
                    gatheringDataStore.Add(new GatheringData { MinimumBaseReward = 1, MaximumBaseReward = 10, TimeMultiplier = 1, MaxTimeAllowed = 5 , Type = resourceType });
            }

            await gatheringDataStore.SaveChanges();
        }

        private async Task SeedBuildingData() 
        {
            var previousBuildingData = buildingDataStore.GetQuery(false);

            var type = BuildingType.Factory;
            if (previousBuildingData.Count() == 3)
                return;

            buildingDataStore.Add(new BuildingData { Type = type, Cost = new List<BuildingPrice> { new BuildingPrice(ResourceType.Wood, 50) }, FactoryParameters = new FactoryParameter { PassiveIncome = 5, ResourceType = ResourceType.Wood } });
            buildingDataStore.Add(new BuildingData { Type = type, Cost = new List<BuildingPrice> { new BuildingPrice(ResourceType.Iron, 50) }, FactoryParameters = new FactoryParameter { PassiveIncome = 5, ResourceType = ResourceType.Iron } });
            buildingDataStore.Add(new BuildingData { Type = type, Cost = new List<BuildingPrice> { new BuildingPrice(ResourceType.Gold, 50) }, FactoryParameters = new FactoryParameter { PassiveIncome = 5, ResourceType = ResourceType.Gold } });

            await buildingDataStore.SaveChanges();
        }

        private async Task SeedTradeData()
        {
            var previousTradeData = tradeDataStore.GetQuery(false);

            if (previousTradeData.Count() == 1)
                return;

            tradeDataStore.Add(new TradeData { RequiredResource = ResourceType.Food, RewardResource = ResourceType.Atk, RiskPercentage = 50, ReturnMultiplier = 2 });

            await tradeDataStore.SaveChanges();
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

        private async Task SeedPlayerScore(Guid userId)
        {
            var scoreboard = scoreboardStore.GetQuery(false);

            if (scoreboard.Any(x => x.Id == userId))
                return;

            scoreboardStore.Add(new Scoreboard { Score = 0, StrategyGameUserId = userId });

            await scoreboardStore.SaveChanges();
        }


        private async Task SeedUsers()
        {
            var userEntity1 = await userManager.FindByIdAsync(User1Id);
            if (userEntity1 != null) 
                return;

            var user1 = new StrategyGameUser()
            {
                Id = Guid.Parse(User1Id),
                UserName = "User1",
                Email = "User1@asd.com",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            await userManager.CreateAsync(user1, "User1Password");
            await userManager.AddClaimAsync(user1, new Claim(Claims.UserId, user1.Id.ToString()));
            await userManager.AddToRoleAsync(user1, Role.User.ToString());

            try
            {
                await SeedPlayerScore(user1.Id);
                await SeedPlayerResources(user1.Id);
                await SeedPlayerBuildings(user1.Id);
            }
            catch (Exception)
            {
                await userManager.DeleteAsync(user1);

                throw;
            }

            var userEntity2 = await userManager.FindByIdAsync(User2Id);
            if (userEntity2 != null)
                return;

            var user2 = new StrategyGameUser()
            {
                Id = Guid.Parse(User2Id),
                UserName = "User2",
                Email = "User2@asd.com",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            await userManager.CreateAsync(user2, "User2Password");
            await userManager.AddClaimAsync(user2, new Claim(Claims.UserId, user2.Id.ToString()));
            await userManager.AddToRoleAsync(user2, Role.User.ToString());

            try
            {
                await SeedPlayerScore(user2.Id);
                await SeedPlayerResources(user2.Id);
                await SeedPlayerBuildings(user2.Id);
            }
            catch (Exception)
            {
                await userManager.DeleteAsync(user2);

                throw;
            }
        }

        private async Task SeedRound() 
        {
            if (roundStore.GetQuery(false).Count() != 0)
                return;

            roundStore.Add(new Round { Current = 1, TicksLeft = 0 });
            await roundStore.SaveChanges();
        }

        public async Task SeedGame()
        {
            await SeedRound();

            await SeedTradeData();
            await SeedResourceData();
            await SeedBuildingData();
            await SeedGatheringData();

            await SeedRoles();
            await SeedUsers();
        }
    }
}
