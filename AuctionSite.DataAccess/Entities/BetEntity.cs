using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionSite.Core.Models
{
    public class BetEntity
    {
        public int Id { get; set; }
        public decimal Price { get; set; }

        public int BuyerId { get; set; }
        public BuyerEntity? Buyer { get; set; }

        public int LotId { get; set; }
        public LotConcreteEntity? Lot { get; set; }

        public int CommentsId { get; set; }
        public CommentsEntity? Comments { get; set; }
    }
}
