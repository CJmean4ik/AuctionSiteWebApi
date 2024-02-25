using CSharpFunctionalExtensions;

namespace AuctionSite.Core.Models
{
    public class ReplyComments
    {
        public int Id { get; }
        public string Text { get; } = string.Empty;
        public string UserName { get; } = string.Empty;

        public int BetId { get; set; }

        private ReplyComments(int id, string text, string userName, int betId)
        {
            Id = id;
            Text = text;
            UserName = userName;
            BetId = betId;
        }

        public static Result<ReplyComments> Create(string text, string userName, int id = 0,int betId = 0)
            => Result.Success(new ReplyComments(id, text, userName, betId));
    }
}