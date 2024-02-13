using CSharpFunctionalExtensions;

namespace AuctionSite.Core.Models
{
    public class Buyer
    {
        public Guid Id { get; }
        public string FirstName { get; } = string.Empty;
        public string SecondName { get; } = string.Empty;

        private Buyer(Guid id, string firstName, string secondName)
        {
            Id = id;
            FirstName = firstName;
            SecondName = secondName;
        }

        public static Result<Buyer> Create(Guid id, string firstName, string secondName)
            => Result.Success(new Buyer(id, firstName, secondName));
    }
}