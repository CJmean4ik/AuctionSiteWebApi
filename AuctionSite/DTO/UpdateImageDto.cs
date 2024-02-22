using System.ComponentModel.DataAnnotations;

namespace AuctionSite.API.DTO
{

    public class UpdateImageDto
    {
        [Required]
        public string OldImageName { get; set; } = string.Empty;

        [Required]
        public IFormFile NewImage { get; set; }

    }
}
