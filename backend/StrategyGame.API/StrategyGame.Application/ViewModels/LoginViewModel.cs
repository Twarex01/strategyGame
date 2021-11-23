using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.Application.ViewModels
{
    public class LoginViewModel
    {
        public LoginViewModel()
        {
        }

        public LoginViewModel(string token)
        {
            Token = token;
        }

        public string Token { get; set; }
    }
}
