using CSharpFunctionalExtensions;

namespace AuctionSite.Core.Models
{
    public class User
    {
        public int Id { get; }
        public string Email { get; } = string.Empty;
        public string Password { get; } = string.Empty;
        public string PasswordSalt { get; } = string.Empty;
        public string Role { get; } = string.Empty;

        public User(int id, string email, string password, string passwordSalt, string role)
        {
            Id = id;
            Email = email;
            Password = password;
            PasswordSalt = passwordSalt;
            Role = role;
        }

        public static Result<User> Create(string email, string password, string passwordSalt, string role, int id = 0)
         => Result.Success<User>(new User(id,email,password,passwordSalt,role));
    }
}