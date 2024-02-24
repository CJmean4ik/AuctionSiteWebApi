using AuctionSite.API.DTO;
using AuctionSite.Application.Services.Image;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuctionSite.API.Controllers
{
    [ApiController]
    [Route("api/v1/lots/images")]
    [Authorize(Roles = "Admin")]
    public class ImageController : Controller
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpGet()]
        [AllowAnonymous]
        public async Task<IActionResult> GetImage([FromQuery] string fileName)
        {
            var result = await _imageService.ReadImageAsync(fileName);

            if (result.IsFailure)
                return Json(new { Status = "500", Error = result.Error });

            return File(result.Value, "image/jpg");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateImage([FromForm] UpdateImageDto imageDto)
        {
            var result = await _imageService.UpdateAsync(imageDto.NewImage, imageDto.OldImageName);

            if (result.IsFailure)
                return Json(new { Status = "500", Error = result.Error });

            return Json(new { Status = "200", Message = result.Value });
        }

        [HttpDelete]
        public async Task<IActionResult> Removemage([FromQuery] string fileName )
        {
            var result = await _imageService.DeleteAsync(fileName);

            if (result.IsFailure)
                return Json(new { Status = "500", Error = result.Error });

            return Json(new { Status = "200", Message = result.Value });
        }

        [HttpPost]
        public async Task<IActionResult> CreateImage([FromForm] IFormFile file)
        {
            var result = await _imageService.CreateImageAsync(file);

            if (result.IsFailure)
                return Json(new { Status = "500", Error = result.Error });

            return Json(new { Status = "200", Message = result.Value });
        }    
    }
}
