using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StrategyGame.Application.Dtos;
using StrategyGame.Application.ServiceInterfaces;
using StrategyGame.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StrategyGame.API.Controllers
{
    [Authorize]
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {
        IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        
        [AllowAnonymous]
        [HttpPost("login")]
        public Task<LoginViewModel> PostLogin(LoginDto loginDto, CancellationToken cancellationToken)
        {
            return userService.LoginUser(loginDto, cancellationToken);
        }
    }
}
