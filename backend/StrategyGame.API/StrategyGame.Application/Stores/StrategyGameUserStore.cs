using Microsoft.EntityFrameworkCore;
using StrategyGame.Common.Stores;
using StrategyGame.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.Application.Stores
{
    public class StrategyGameUserStore : EntityStore<StrategyGameUser>
    {
        public StrategyGameUserStore(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
