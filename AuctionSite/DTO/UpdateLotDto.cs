using AuctionSite.DataAccess.Entities;
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
        public string? FullDescription { get; set; }
        public int? DurationSale { get; set; }
        public LotStatus? LotStatus { get; set; }
        
    }
}