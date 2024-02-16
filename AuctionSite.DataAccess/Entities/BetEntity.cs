namespace AuctionSite.Core.Models
{
    public class BetEntity
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }

        public Guid BuyerId { get; set; }
        public BuyerEntity? Buyer { get; set; }

        public Guid LotId { get; set; }
        public LotConcreteEntity? Lot { get; set; }

        public Guid CommentsId { get; set; }
        public CommentsEntity? Comments { get; set; }
    }
}
