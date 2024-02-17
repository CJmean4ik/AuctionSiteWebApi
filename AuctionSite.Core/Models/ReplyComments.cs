using CSharpFunctionalExtensions;

namespace AuctionSite.Core.Models
{
    public class ReplyComments
    {
        public int Id { get; }
        public string Text { get; } = string.Empty;
        public string UserName { get; } = string.Empty;

        private ReplyComments(int id, string text, string userName)
        {
            Id = id;
            Text = text;
            UserName = userName;
        }

        public static Result<ReplyComments> Create(string text, string userName, int id = 0)
            => Result.Success(new ReplyComments(id, text, userName));
    }
}