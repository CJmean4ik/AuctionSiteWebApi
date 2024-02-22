using CSharpFunctionalExtensions;

namespace AuctionSite.Core.Models
{
    public class Bet
    {
        private readonly List<ReplyComments> _replyComments = new List<ReplyComments>();

        public int Id { get; }
        public decimal Price { get; }
        public string BuyerName { get; set; }
        public string BuyerLastName { get; set; }
        public string Comments { get; set; }
        public IReadOnlyCollection<ReplyComments> ReplyComments => _replyComments;

        public void AddReplyComments(List<ReplyComments> comments) => _replyComments.AddRange(comments);

        private Bet(int id, decimal price,string buyerName, string buyerLastName,string comments)
        {
            Id = id;
            Price = price;
            BuyerName = buyerName;
            BuyerLastName = buyerLastName;
            Comments = comments;
        }

        public static Result<Bet> Create(decimal price, string buyerName, string buyerLastName,string comments, int id = 0)
            => Result.Success(new Bet(id, price, buyerName, buyerLastName,comments));
    }
}
