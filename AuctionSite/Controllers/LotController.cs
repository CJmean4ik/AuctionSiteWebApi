using AuctionSite.Application.DTO;
using AuctionSite.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuctionSite.API.Controllers
{
    [ApiController]
    [Route("api/v1/lots")]
    public class LotController : Controller
    {
        private readonly LotService _lotService;
        public LotController(LotService lotService)
        {
            _lotService = lotService;
        }

        [HttpGet]
        public async Task<ActionResult> GetLots()
        {
            var result = await _lotService.GetLotsAsync();
  
            return CreateJsonResult("200", new { Count = result.Value.Count, List = result.Value });
        }

        [HttpPut]
        public async Task<ActionResult> UpdateLots([FromBody] UpdateLotDto updateLotDto)
        {
            var result = await _lotService.UpdateLotAsync(updateLotDto);

            if (result.IsFailure)
                return CreateJsonResult("500", result.Error);

            return CreateJsonResult("200", result.Value);
        }

        [HttpDelete]
        public async Task<ActionResult> RemoveLots()
        {
            var result = await _lotService.GetLotsAsync();

            if (result.IsFailure)
                return CreateJsonResult("500", result.Error);

            return CreateJsonResult("200", result.Value);
        }

        [HttpPost]
        public async Task<ActionResult> AddLots()
        {
            var result = await _lotService.GetLotsAsync();

            if (result.IsFailure)
                return CreateJsonResult("500", result.Error);

            return CreateJsonResult("200", result.Value);
        }

        [HttpGet("/by/id")]
        public async Task<ActionResult> GetByIdLots()
        {
            var result = await _lotService.GetLotsAsync();

            if (result.IsFailure)
                return CreateJsonResult("500", result.Error);

            return CreateJsonResult("200", result.Value);
        }

        [HttpGet("/by/page")]
        public async Task<ActionResult> GetByPageLots()
        {
            var result = await _lotService.GetLotsAsync();

            if (result.IsFailure)
                return CreateJsonResult("500", result.Error);

            return CreateJsonResult("200", result.Value);
        }

        private ActionResult CreateJsonResult(string statusName, object obj) =>
        Json(new
        {
            StatusName = statusName,
            Time = $"{DateTime.Now}",
            Data = obj
        });

    }
}
