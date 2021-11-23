using Microsoft.EntityFrameworkCore;
using StrategyGame.Common.Stores;
using StrategyGame.Domain.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.Application.Stores
{
    public class BuildingStore : EntityStore<Building>
    {
        public BuildingStore(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
