using CSharpFunctionalExtensions;

namespace AuctionSite.Core.Models
{
    public class ReplyComments
    {
        public Guid Id { get; }
        public string Text { get; } = string.Empty;
        public string UserName { get; } = string.Empty;

        private ReplyComments(Guid id, string text, string userName)
        {
            Id = id;
            Text = text;
            UserName = userName;
        }

        public static Result<ReplyComments> Create(Guid id, string text, string userName)
            => Result.Success(new ReplyComments(id, text, userName));
    }
}