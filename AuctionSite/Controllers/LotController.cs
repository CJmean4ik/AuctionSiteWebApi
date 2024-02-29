using AuctionSite.API.DTO;
using AuctionSite.Application.Services;
using AuctionSite.Application.Services.Image;
using AuctionSite.Core.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
// чето там изменения 3
namespace AuctionSite.API.Controllers
{
    [ApiController]
    [Route("api/v1/lots")]
    public class LotController : Controller
    {
        private readonly LotService _lotService;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;

        public LotController(LotService lotService,            
                             IImageService imageService,
                             IMapper mapper)
        {
            _lotService = lotService;
            _imageService = imageService;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetLots([FromQuery] int page = 1)
        {
            var result = await _lotService.GetLotsAsync(page);

            return CreateJsonResult("200", new { Count = result.Value.Count, List = result.Value });
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateLots([FromForm] UpdateLotDto updateLotDto)
        {
            var specifiLot = _mapper.Map<SpecificLot>(updateLotDto);

            var result = await _lotService.UpdateSpecificLotAsync(specifiLot);

            if (result.IsFailure)
                return CreateJsonResult("500", result.Error);

            return CreateJsonResult("200", result.Value);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> RemoveLots([FromQuery] int id)
        {
            var result = await _lotService.RemoveLotAsync(id);

            if (result.IsFailure)
                return CreateJsonResult("500", result.Error);

            return CreateJsonResult("200", result.Value);
        }

        [HttpPost]
        [Authorize(Roles = "Buyer")]
        public async Task<ActionResult> AddLots([FromForm] CreateLotDto createLotDto)
        {
            var imagePreviewResult = await _imageService.CreateImageAsync(createLotDto.ImagePreview!);

            if (imagePreviewResult.IsFailure)
                return CreateJsonResult("500", imagePreviewResult.Error);

            var buyerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var buyerName = User.FindFirstValue(ClaimTypes.Name);
            var buyerLastName = User.FindFirstValue(ClaimTypes.Surname);

            createLotDto.ImagePreviewName = imagePreviewResult.Value;
            createLotDto.BuyerId = buyerId;
            createLotDto.BuyerLastName = buyerLastName;
            createLotDto.BuyerFirstName = buyerName;

            var lot = _mapper.Map<CreateLotDto, SpecificLot>(createLotDto);
            
            var result = await _lotService.AddLotAsync(lot);

            if (result.IsFailure)
                return CreateJsonResult("500", result.Error);

            return CreateJsonResult("200", new
            {
                LotResult = result.Value,
                PreviewImageResult = imagePreviewResult.Value,
            });
        }

        [HttpGet("specific")]
        [AllowAnonymous]
        public async Task<ActionResult> GetByIdLots([FromQuery] int id)
        {
            var result = await _lotService.GetSpecificLotAsync(id);

            if (result.IsFailure)
                return CreateJsonResult("500", result.Error);

            return CreateJsonResult("200", result.Value);
        }

        [HttpGet("all/buyer")]
        [Authorize(Roles = "Buyer")]
        public async Task<ActionResult> GetUserLots([FromQuery]int page = 1, [FromQuery] int limit = 10)
        {
            var buyerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var result = await _lotService.GetUserLotsAsync(buyerId, page, limit);

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
