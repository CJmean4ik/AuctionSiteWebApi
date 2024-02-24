namespace AuctionSite.DataAccess.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PasswordSalt { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

        public List<SpecificLotEntity>? LotEntities { get; set; }
        public List<BetEntity>? Bets { get; set; }
    }
}