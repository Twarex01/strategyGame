using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StrategyGame.Application.Dtos;
using StrategyGame.Application.Options;
using StrategyGame.Application.ServiceInterfaces;
using StrategyGame.Application.ViewModels;
using StrategyGame.Common.Constants;
using StrategyGame.Domain;
using StrategyGame.Entities.Domain;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StrategyGame.Application.Services
{
    public class UserService : IUserService
    {
        private readonly SignInManager<StrategyGameUser> signInManager;
        private readonly UserManager<StrategyGameUser> userManager;

        private readonly JwtTokenOptions jwtTokenOptions;

        public UserService(SignInManager<StrategyGameUser> signInManager, UserManager<StrategyGameUser> userManager, IOptionsSnapshot<JwtTokenOptions> jwtTokenOptionsSnapshot)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.jwtTokenOptions = jwtTokenOptionsSnapshot.Value;
        }

        public async Task<LoginViewModel> LoginUser(LoginDto loginDto, CancellationToken cancellationToken)
        {
            var user = await ValidateExists(loginDto.Email);

            var result = await signInManager.PasswordSignInAsync(user, loginDto.Password, false, false);

            if (!result.Succeeded)
            {
                throw new Exception(ErrorMessages.FailedLogin);
            }

            return await GenerateTokens(user);
        }

        private async Task<StrategyGameUser> ValidateExists(string emailAddress)
        {
            var existingUser = await userManager.FindByEmailAsync(emailAddress);

            if (existingUser == null)
            {
                throw new Exception(ErrorMessages.NotFound);
            }

            return existingUser;
        }

        private async Task<LoginViewModel> GenerateTokens(StrategyGameUser user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenOptions.Key));

            var expires = DateTime.Now.AddMinutes(Convert.ToInt32(jwtTokenOptions.ExpireMinutes));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = await userManager.GetClaimsAsync(user);

            claims.Add(new Claim(Claims.UserId, user.Id.ToString()));

            var token = new JwtSecurityToken(
                jwtTokenOptions.Issuer,
                jwtTokenOptions.Audience,
                claims,
                expires: expires,
                signingCredentials: creds);

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new LoginViewModel(jwtToken);
        }
    }
}
