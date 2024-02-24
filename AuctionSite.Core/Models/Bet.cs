using CSharpFunctionalExtensions;
using System;

namespace AuctionSite.Core.Models
{
    public class Bet
    {
        private readonly List<ReplyComments> _replyComments = new List<ReplyComments>();

        public int Id { get; }
        public decimal Price { get; }
        public string BuyerName { get; }
        public string BuyerLastName { get; }
        public string Comments { get;}
        public int LotId { get; }
        public int UserId { get; }
        public IReadOnlyCollection<ReplyComments> ReplyComments => _replyComments;

        public void AddReplyComments(List<ReplyComments> comments) => _replyComments.AddRange(comments);

        private Bet(int id, decimal price,string buyerName, string buyerLastName,string comments,int lotId,int userId)
        {
            Id = id;
            LotId = lotId;
            Price = price;
            BuyerName = buyerName;
            BuyerLastName = buyerLastName;
            Comments = comments;
            UserId = userId;
        }

        public static Result<Bet> Create(string buyerName, string buyerLastName, string comments, decimal price, int id = 0, int lotId =0,int userId = 0)
            => Result.Success(new Bet(id, price, buyerName, buyerLastName,comments,lotId,userId));
    }
}
