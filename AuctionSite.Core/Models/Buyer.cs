using CSharpFunctionalExtensions;

namespace AuctionSite.Core.Models
{
    public class Buyer
    {
        public int Id { get; }
        public string FirstName { get; } = string.Empty;
        public string SecondName { get; } = string.Empty;

        private Buyer(int id, string firstName, string secondName)
        {
            Id = id;
            FirstName = firstName;
            SecondName = secondName;
        }

        public static Result<Buyer> Create(string firstName, string secondName, int id = 0)
                     => Result.Success(new Buyer(id, firstName, secondName));
        



    }
}