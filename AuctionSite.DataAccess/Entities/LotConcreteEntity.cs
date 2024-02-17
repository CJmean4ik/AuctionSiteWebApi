using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionSite.Core.Models
{
    [Table("LotConcrete")]
    public class LotConcreteEntity : LotEntity
    {
        public string FullDescription { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int DurationSale { get; set; }
        public string FullImage { get; set; } = string.Empty;
        public decimal MaxPrice { get; set; }
        public LotStatus LotStatus { get; set; }

        public List<BetEntity>? Bets { get; set; }
    }

    public enum LotStatus
    {
        Closed,
        Sold,
        Active
    }
}
