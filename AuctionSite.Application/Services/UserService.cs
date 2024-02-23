using AuctionSite.Application.Services.Password;
using AuctionSite.Core.Contracts.Repositories.Specific;
using AuctionSite.Core.Models;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace AuctionSite.Application
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<Result<string>> LoginUser(User user, HttpContext httpContext)
        {
            var userForLogin = await _userRepository.GetByEmail(user.Email);

            if (!_passwordHasher.Decryption(user.Password, userForLogin.Value.User.PasswordSalt, userForLogin.Value.User.Password))
                return Result.Failure<string>("Wrong password");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, userForLogin.Value.User.Email),
                new Claim(ClaimTypes.Role, userForLogin.Value.User.Role),
                new Claim(ClaimTypes.NameIdentifier, userForLogin.Value.Id.ToString())
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
            return Result.Success("Login Success!");
        }
        public async Task<Result<string>> LogOutUser(HttpContext httpContext)
        {
            try
            {
                await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return Result.Success("User log-out!");
            }
            catch (Exception ex)
            {
                return Result.Failure<string>(ex.Message);
            }
        }
        public async Task<Result<string>> RegisterUser(Buyer buyer)
        {
            var paswordHashingResult = _passwordHasher.Encryption(buyer.User.Password);

            buyer.User.SetSalt(paswordHashingResult.salt);
            buyer.User.SetPassword(paswordHashingResult.hash);

            var result = await _userRepository.AddAsync(buyer);
  
            return result;
        }
    }
}