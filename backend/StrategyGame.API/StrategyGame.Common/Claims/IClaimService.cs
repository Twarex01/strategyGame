using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.Common.Claims
{
    public interface IClaimService
    {
        public Guid GetUserId();

        public ClaimsPrincipal User { get; }
    }
}
