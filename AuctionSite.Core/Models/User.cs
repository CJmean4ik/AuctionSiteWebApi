using CSharpFunctionalExtensions;

namespace AuctionSite.Core.Models
{
    public class User
    {
        private string _hash;
        private string _salt;

        public int Id { get; }
        public string Email { get; } = string.Empty;        
        public string Role { get; private set; } = string.Empty;
        public string Password => _hash;
        public string PasswordSalt => _salt;

        public void SetPassword(string password) => _hash = password;
        public void SetSalt(string salt) => _salt = salt;

        public User(int id, string email, string role)
        {
            Id = id;
            Email = email;
            Role = role;
        }

        public static Result<User> Create(string email, string role, int id = 0)
         => Result.Success<User>(new User(id,email,role));
    }
}