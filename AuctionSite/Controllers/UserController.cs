using AuctionSite.API.DTO;
using AuctionSite.Application;
using AuctionSite.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AuctionSite.API.Controllers
{
    [ApiController]
    public class UserController : Controller
    {
        private UserService _usersService;

        public UserController(UserService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost("/login")]
        public async Task<ActionResult> SignIn([FromForm] UserAuthorizePostDto userAuthorize)
        {
            var user = AuctionSite.Core.Models.User.Create(userAuthorize.Email, "").Value;
            user.SetPassword(userAuthorize.EnteredPassword);

            var result = await _usersService.LoginUser(user, HttpContext);

            if (result.IsFailure)           
                return Json(new {Error = result.Error  });
            
            return Json(new 
            {
                Message = "Success",
                Claims = new 
                {
                    Email = user.Email,
                    Role = result.Value
                } 
            });
        }

        [HttpPost("buyer/registration")]
        public async Task<ActionResult> SignUp([FromForm] UserSignUpPostDto userAuthorize)
        {
            var user = AuctionSite.Core.Models.User.Create(userAuthorize.Email, "Buyer").Value!;
            var buyer = Buyer.Create(userAuthorize.FirstName, userAuthorize.SecondName, user).Value!;
            buyer.User.SetPassword(userAuthorize.Password);

            var result = await _usersService.RegisterUser(buyer);

            if (result.IsFailure) 
                return Json(new { Status = 500, Error = result.Error });

            user.SetPassword(userAuthorize.Password);

            result = await _usersService.LoginUser(user,HttpContext);

            return Json(result.Value);
        }

        [HttpGet("buyer/logout")]
        [Authorize(Roles ="Buyer,Admin")]
        public async Task<ActionResult> LogOut()
        {
            var result = await _usersService.LogOutUser(HttpContext);

            if (result.IsFailure)
                return Json(new { Status = 500, Error = result.Error });

            return Json(result.Value);
        }
    }
}
