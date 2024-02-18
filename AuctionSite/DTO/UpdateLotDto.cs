using System.ComponentModel.DataAnnotations;

namespace AuctionSite.API.DTO
{
    public class UpdateLotDto
    {
        [Required(ErrorMessage = "Не вказаний id")]
        public int? Id { get; set; }

        public string? Name { get; set; }
        public string? ShortDescription { get; set; }
        public string? CategoryName { get; set; }

        /*
        public string? FullDescription { get; set; } = string.Empty;
        public DateTime? EndDate { get; set; }
        public int? DurationSale { get; set; }
        public string? FullImage { get; set; } = string.Empty;
        public LotStatus? LotStatus { get; set; }
        */
    }
}