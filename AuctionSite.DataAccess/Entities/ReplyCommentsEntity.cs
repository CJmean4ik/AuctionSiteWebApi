namespace AuctionSite.DataAccess.Entities
{
    public class ReplyCommentsEntity
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;

        public int BetId { get; set; }
        public BetEntity Bet { get; set; }
    }
}