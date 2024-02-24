using AuctionSite.API.DTO;
using AuctionSite.Application.Services;
using AuctionSite.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuctionSite.API.Controllers
{
    [ApiController]
    [Route("api/v1/bets")]
    [Authorize(Roles = "Buyer, Admin")]
    public class BetController : Controller
    {
        private readonly BetService _betService;

        public BetController(BetService betService)
        {
            _betService = betService;
        }

        [HttpPost]
        public async Task<IActionResult> MakeBet([FromForm] CreateBetDto createBet)
        {
            var buyerName = User.FindFirstValue(ClaimTypes.Name);
            var buyerLastName = User.FindFirstValue(ClaimTypes.Surname);
            var buyerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var bet = Bet.Create(buyerName,buyerLastName,createBet.Comments,createBet.Price,lotId: createBet.LotId,userId: buyerId).Value;
            var result = await _betService.CreateBet(bet);

            if(result.IsFailure)
                return Json(new { status = "500", result.Error });

            return Json(new { status = "200", message = result.Value });
        }

        [HttpGet("all/buyer")]
        public async Task<IActionResult> GetAllBet()
        {
            var buyerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var result = await _betService.GetBueyrBets(buyerId);

            if (result.IsFailure)
                return Json(new { status = "500", result.Error });

            return Json(new { status = "200", bet = result.Value });
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetBets([FromQuery] ReadBetDto readBet)
        {
            var result = await _betService.GetBets(readBet.Page.Value!,readBet.Limit.Value!,readBet.LotId);

            if (result.IsFailure)
                return Json(new { status = "500", result.Error });

            return Json(new {status= "200", Count = result.Value.Count, List = result.Value.OrderByDescending(b => b.Price) });
        }

        [HttpGet("max")]
        [AllowAnonymous]
        public async Task<IActionResult> GetMaxBet([FromQuery] int lotId)
        {
            var result = await _betService.GetMaxBet(lotId);

            if (result.IsFailure)
                return Json(new { status = "500", result.Error });

            return Json(new { status = "200", bet = result.Value});
        }

       

    }
}
