using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StrategyGame.API.Controllers
{
    [Authorize]
    [Route("api/test")]
    [ApiController]
    public class TestController : Controller
    {
        //public Task<IActionResult> TestController(CancellationToken cancellationToken) 
        //{
        //}
    }
}
