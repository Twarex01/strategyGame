using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.Application.Hubs
{
    public class RoundHub : Hub<IRoundHubClient>
    {
        public async override Task OnConnectedAsync()
        {
            var claim = Context.User.FindFirst(Common.Constants.Claims.UserId);
            var userId = Guid.Parse(claim.Value);

            await Groups.AddToGroupAsync(Context.ConnectionId, userId.ToString());

            await base.OnConnectedAsync();
        }
    }
}
