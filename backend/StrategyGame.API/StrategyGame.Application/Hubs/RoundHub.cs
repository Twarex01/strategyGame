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
    }
}
