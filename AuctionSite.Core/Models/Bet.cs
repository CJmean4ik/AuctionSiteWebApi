using CSharpFunctionalExtensions;

namespace AuctionSite.Core.Models
{
    public class Bet
    {
        private readonly List<Comments> _comments = new List<Comments>();

        public Guid Id { get; }
        public decimal Price { get; }
        public Buyer Buyer { get; }
        public IReadOnlyCollection<Comments> Comments => _comments;

        public void AddComments(List<Comments> comments) => _comments.AddRange(comments);

        private Bet(Guid id, decimal price, Buyer buyer)
        {
            Id = id;
            Price = price;
            Buyer = buyer;
        }

        public static Result<Bet> Create(Guid id, decimal price, Buyer buyer)
            => Result.Success(new Bet(id, price, buyer));
    }
}
