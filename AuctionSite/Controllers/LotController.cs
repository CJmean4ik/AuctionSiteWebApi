using AuctionSite.API.Contracts;
using AuctionSite.API.Services.ErrorValidation;
using AuctionSite.API.DTO;
using AuctionSite.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using AuctionSite.Core.Models;
using AutoMapper;

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
        public async Task<ActionResult> GetLots()
        {
            var result = await _lotService.GetLotsAsync();        

            return CreateJsonResult("200", new { Count = result.Value.Count, List = result.Value });
        }

        [HttpPut]
        public async Task<ActionResult> UpdateLots([FromBody] UpdateLotDto updateLotDto)
        {
            var lot = Lot.Create(updateLotDto.Name,
                updateLotDto.ShortDescription,
                updateLotDto.CategoryName,null,updateLotDto.Id!.Value).Value;

            var result = await _lotService.UpdateLotAsync(lot);

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
       

        /*
        [HttpGet("/by/id")]
        public async Task<ActionResult> GetByIdLots([FromQuery]int id)
        {
            var result = await _lotService.GetConcreteLotAsync(id);

            if (result.IsFailure)
                return CreateJsonResult("500", result.Error);

            return CreateJsonResult("200", result.Value);
        }
        */

        /*
        [HttpGet("/by/page")]
        public async Task<ActionResult> GetByPageLots()
        {
            var result = await _lotService.GetLotsAsync();

            if (result.IsFailure)
                return CreateJsonResult("500", result.Error);

            return CreateJsonResult("200", result.Value);
        }
        */
        private ActionResult CreateJsonResult(string statusName, object obj) =>
        Json(new
        {
            StatusName = statusName,
            Time = $"{DateTime.Now}",
            Data = obj
        });

    }
}
