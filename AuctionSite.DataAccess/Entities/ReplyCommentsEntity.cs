
namespace AuctionSite.Core.Models
{
    public class ReplyCommentsEntity
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;

        public int CommentsId { get; set; }
        public CommentsEntity? Comments { get; set; }
    }
}