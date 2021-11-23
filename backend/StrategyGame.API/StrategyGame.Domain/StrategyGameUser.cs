using Microsoft.AspNetCore.Identity;
using StrategyGame.Common.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.Domain
{
    public class StrategyGameUser : IdentityUser<Guid>, IEntity
    {
    }
}
