namespace AuctionSite.Core.Models
{
    public class BuyerEntity
    {
        public Guid? Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string SecondName { get; set; } = string.Empty;

        public List<BetEntity>? Bets { get; set; }

        public Guid UserId { get; set; }
        public UserEntity? User { get; set; }

    }
}