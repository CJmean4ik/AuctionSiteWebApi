using CSharpFunctionalExtensions;

namespace AuctionSite.Core.Models
{
    public class Comments
    {
        private readonly List<ReplyComments> _replyComments = new List<ReplyComments>();

        public int Id { get; }
        public string Text { get; } = string.Empty;
        public List<ReplyComments> ReplyComments => _replyComments;

        public void AddReplyComments(List<ReplyComments> replyComments) => _replyComments.AddRange(replyComments);

        private Comments(int id, string text)
        {
            Id = id;
            Text = text;
        }

        public static Result<Comments> Create(string text, int id = 0) => Result.Success(new Comments(id, text));
    }
}