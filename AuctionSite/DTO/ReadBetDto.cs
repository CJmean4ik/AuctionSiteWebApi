using System.ComponentModel.DataAnnotations;

namespace AuctionSite.API.DTO
{
    public class ReadBetDto
    {
        [Required(ErrorMessage ="Іd лота повине бути вказане!")]
        public int LotId { get; set; }
        public int? Page { get; set; } = 1;
        public int? Limit { get; set; } = 5;
    }
}
