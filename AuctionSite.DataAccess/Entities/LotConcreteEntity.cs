namespace AuctionSite.Core.Models
{
    public class LotConcreteEntity
    {
        public Guid Id { get; set; }
        public string FullDescription { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime HaveTime { get; set; }
        public decimal MaxPrice { get; set; }
        public bool IsSold { get; set; }

        public List<BetEntity>? Bets { get; set; }

        public Guid LotId { get; set; }
        public LotEntity? Lot { get; set; }
    }
}