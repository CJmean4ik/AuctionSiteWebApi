using CSharpFunctionalExtensions;

namespace AuctionSite.Core.Models
{
    public class Bet
    {
        private readonly List<Comments> _comments = new List<Comments>();

        public int Id { get; }
        public decimal Price { get; }
        public Buyer Buyer { get; }
        public IReadOnlyCollection<Comments> Comments => _comments;

        public void AddComments(List<Comments> comments) => _comments.AddRange(comments);

        private Bet(int id, decimal price, Buyer buyer)
        {
            Id = id;
            Price = price;
            Buyer = buyer;
        }

        public static Result<Bet> Create(decimal price, Buyer buyer, int id = 0)
            => Result.Success(new Bet(id, price, buyer));
    }
}
