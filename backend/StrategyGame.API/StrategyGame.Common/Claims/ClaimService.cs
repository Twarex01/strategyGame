using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace StrategyGame.Common.Claims
{
    public class ClaimService : IClaimService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public ClaimService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public ClaimsPrincipal User => httpContextAccessor?.HttpContext?.User;

        public Guid GetUserId()
        {
            var claim = User.FindFirst(Constants.Claims.UserId);

            return Guid.Parse(claim.Value);
        }
    }
}
