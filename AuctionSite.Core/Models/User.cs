using CSharpFunctionalExtensions;

namespace AuctionSite.Core.Models
{
    public class User
    {
        public Guid Id { get; }
        public string Email { get; } = string.Empty;
        public string Password { get; } = string.Empty;
        public string PasswordSalt { get; } = string.Empty;
        public string Role { get; } = string.Empty;

        public User(Guid id, string email, string password, string passwordSalt, string role)
        {
            Id = id;
            Email = email;
            Password = password;
            PasswordSalt = passwordSalt;
            Role = role;
        }

        public static Result<User> Create(Guid id, string email, string password, string passwordSalt, string role)
         => Result.Success<User>(new User(id,email,password,passwordSalt,role));
    }
}