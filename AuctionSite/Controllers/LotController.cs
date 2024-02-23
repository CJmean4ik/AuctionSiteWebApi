using AuctionSite.API.Contracts;
using AuctionSite.API.Services.ErrorValidation;
using AuctionSite.API.DTO;
using AuctionSite.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using AuctionSite.Core.Models;
using AutoMapper;
using AuctionSite.Application.Services.Image;

namespace AuctionSite.API.Controllers
{
    [ApiController]
    [Route("api/v1/lots")]
    public class LotController : Controller
    {
        private readonly LotService _lotService;
        private readonly IImageService _imageService;
        private readonly IErrorValidationHandler<List<ErrorModel>, ModelStateDictionary> _errorHandler;
        private readonly IMapper _mapper;

        public LotController(LotService lotService,
                             IErrorValidationHandler<List<ErrorModel>, ModelStateDictionary> errorHandler,
                             IImageService imageService,
                             IMapper mapper)
        {
            _lotService = lotService;
            _errorHandler = errorHandler;
            _imageService = imageService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetLots([FromQuery]int page = 1)
        {
            var result = await _lotService.GetLotsAsync(page);        

            return CreateJsonResult("200", new { Count = result.Value.Count, List = result.Value });
        }

        [HttpPut]
        public async Task<ActionResult> UpdateLots([FromForm] UpdateLotDto updateLotDto)
        {
            var specifiLot = _mapper.Map<SpecificLot>(updateLotDto);
             
            var result = await _lotService.UpdateSpecificLotAsync(specifiLot);

            if (result.IsFailure)
                return CreateJsonResult("500", result.Error);
         
            return CreateJsonResult("200", result.Value);
        }
     
        [HttpDelete]
        public async Task<ActionResult> RemoveLots([FromQuery]int id)
        {            
            var result = await _lotService.RemoveLotAsync(id);

            if (result.IsFailure)
                return CreateJsonResult("500", result.Error);

            return CreateJsonResult("200", result.Value);
        }
            
        [HttpPost]
        public async Task<ActionResult> AddLots([FromForm] CreateLotDto createLotDto)
        {
            var lot = _mapper.Map<CreateLotDto, Lot>(createLotDto);

            var result = await _lotService.AddLotAsync(lot);

            if (result.IsFailure)
                return CreateJsonResult("500", result.Error);

            return CreateJsonResult("200", result.Value);
        }
      
        [HttpGet("specific")]
        public async Task<ActionResult> GetByIdLots([FromQuery]int id)
        {
            var result = await _lotService.GetSpecificLotAsync(id);

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
