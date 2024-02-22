using AuctionSite.API.DTO;
using AuctionSite.Application.Model;
using AuctionSite.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuctionSite.API.Controllers
{
    [ApiController]
    [Route("api/v1/lots/images")]
    public class ImageController : Controller
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpGet("{imageType}")]
        public async Task<IActionResult> GetImage([FromQuery] string fileName, ImageType imageType)
        {
            var result = await _imageService.ReadImageAsync(fileName, imageType);

            if (result.IsFailure)
                return Json(new { Status = "500", Error = result.Error });

            return File(result.Value, "image/jpg");
        }

        [HttpPut("{imageType}")]
        public async Task<IActionResult> UpdateImage([FromForm] UpdateImageDto imageDto, ImageType imageType)
        {
            var result = await _imageService.UpdateAsync(imageDto.NewImage, imageDto.OldImageName, imageType);

            if (result.IsFailure)
                return Json(new { Status = "500", Error = result.Error });

            return Json(new { Status = "200", Message = result.Value });
        }

        [HttpDelete("{imageType}")]
        public async Task<IActionResult> Removemage([FromQuery] string fileName,ImageType imageType)
        {
            var result = await _imageService.DeleteAsync(fileName, imageType);

            if (result.IsFailure)
                return Json(new { Status = "500", Error = result.Error });

            return Json(new { Status = "200", Message = result.Value });
        }

        [HttpPost("{imageType}")]
        public async Task<IActionResult> CreateImage([FromForm] IFormFile file, ImageType imageType)
        {
            var result = await _imageService.CreateImageAsync(file, imageType);

            if (result.IsFailure)
                return Json(new { Status = "500", Error = result.Error });

            return Json(new { Status = "200", Message = result.Value });
        }    
    }
}
