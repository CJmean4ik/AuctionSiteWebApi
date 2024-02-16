namespace AuctionSite.Core.Models
{
    public class ReplyCommentsEntity
    {
        public Guid Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;

        public Guid CommentsId { get; set; }
        public CommentsEntity? Comments { get; set; }
    }
}