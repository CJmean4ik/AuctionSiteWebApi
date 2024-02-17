using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionSite.Core.Models
{
    [Table("Buyer")]
    public class BuyerEntity : UserEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string SecondName { get; set; } = string.Empty;

        public List<BetEntity>? Bets { get; set; }
    }
}