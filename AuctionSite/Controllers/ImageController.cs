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

        [HttpGet("preview")]
        public async Task<IActionResult> GetPreviewImage([FromQuery] string fileName)
        {
            var result = await _imageService.GetImage<Stream>(fileName, ImageType.PreviewImage);

            if (result.IsFailure)
                return Json("500", result.Error);

            return File(result.Value,"image/jpg");
        }

        [HttpPost("preview")]
        public async Task<IActionResult> CreatePreviewImage([FromForm] IFormFile file)
        {
            var result = await _imageService.SaveImageAsync(file, ImageType.PreviewImage);

            if (result.IsFailure)
                return Json("500", result.Error);

            return Json("200",result.Value);
        }
    }
}
