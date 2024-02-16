namespace AuctionSite.Core.Models
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PasswordSalt { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;


        public Guid BuyerId { get; set; }
        public BuyerEntity? Buyer{ get; set; }
    }
}