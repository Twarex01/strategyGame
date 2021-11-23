using StrategyGame.Application.Dtos;
using StrategyGame.Application.ServiceInterfaces;
using StrategyGame.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StrategyGame.Application.Services
{
    public class UserService : IUserService
    {
        public Task<LoginViewModel> LoginUser(LoginDto loginDto, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
