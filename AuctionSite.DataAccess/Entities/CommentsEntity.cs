namespace AuctionSite.DataAccess.Entities
{
    public class CommentsEntity
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;

        public int BetId { get; set; }
        public BetEntity? Bet { get; set; }

        public List<ReplyCommentsEntity>? ReplyComments { get; set; }
    }
}