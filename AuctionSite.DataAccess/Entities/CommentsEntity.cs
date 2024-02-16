namespace AuctionSite.Core.Models
{
    public class CommentsEntity
    {     
        public Guid Id { get; set; }
        public string Text { get; set; } = string.Empty;

        public Guid BetId { get; set; }
        public BetEntity? Bet { get; set; }

        public List<ReplyCommentsEntity>? ReplyComments { get; set; }
    }
}