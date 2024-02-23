using AuctionSite.API.DTO;
using AuctionSite.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuctionSite.API.Controllers
{
    [ApiController]
    [Route("api/v1/bets")]
    public class BetController : Controller
    {
        private readonly BetService _betService;

        public BetController(BetService betService)
        {
            _betService = betService;
        }

        [HttpPost]
        [Authorize(Roles ="Buyer, Admin")]
        public async Task<IActionResult> MakeBet([FromForm] CreateBetDto createBet)
        {
            return null;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetBets([FromQuery] ReadBetDto readBet)
        {
            var result = await _betService.GetBets(readBet.Page.Value!,readBet.Limit.Value!,readBet.LotId);

            if (result.IsFailure)
                return Json(new { status = "500", result.Error });

            return Json(new {status= "200", Count = result.Value.Count, List = result.Value });
        }
    }
}
