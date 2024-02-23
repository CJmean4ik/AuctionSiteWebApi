using CSharpFunctionalExtensions;

namespace AuctionSite.Core.Models
{
    public class Buyer
    {
        public int Id { get; }
        public string FirstName { get; } = string.Empty;
        public string SecondName { get; } = string.Empty;
        public User? User { get; }

        private Buyer(int id, string firstName, string secondName,User user)
        {
            Id = id;
            FirstName = firstName;
            SecondName = secondName;
            User = user;
        }

        public static Result<Buyer> Create(string firstName, string secondName,User? user = null, int id = 0)
                     => Result.Success(new Buyer(id, firstName, secondName, user));
        



    }
}