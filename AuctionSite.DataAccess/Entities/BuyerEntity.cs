using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionSite.DataAccess.Entities
{
    [Table("Buyer")]
    public class BuyerEntity : UserEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string SecondName { get; set; } = string.Empty;
    }
}