using AuctionSite.Core.Contracts.Repositories.Specific;
using AuctionSite.Core.Models;
using AuctionSite.Infrastructure.Password;
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

            if (userForLogin.IsFailure)
                return Result.Failure<string>(userForLogin.Error);

            try
            {
                if (!_passwordHasher.Decryption(user.Password, userForLogin.Value.User.PasswordSalt, userForLogin.Value.User.Password))
                    return Result.Failure<string>("Wrong password");

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, userForLogin.Value.User.Email),
                    new Claim(ClaimTypes.Name, userForLogin.Value.FirstName),
                    new Claim(ClaimTypes.Surname, userForLogin.Value.SecondName),
                    new Claim(ClaimTypes.Role, userForLogin.Value.User.Role),
                    new Claim(ClaimTypes.NameIdentifier, userForLogin.Value.Id.ToString())
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                return Result.Success(userForLogin.Value.User.Role);
            }
            catch (Exception ex)
            {
                return Result.Failure<string>(ex.Message);
            }
            
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
            try
            {
                var paswordHashingResult = _passwordHasher.Encryption(buyer.User.Password);

                buyer.User.SetSalt(paswordHashingResult.salt);
                buyer.User.SetPassword(paswordHashingResult.hash);

                var result = await _userRepository.AddAsync(buyer);                
                return result;
            }
            catch (Exception ex)
            {
                return Result.Failure<string>(ex.Message);
            }
        }
    }
}