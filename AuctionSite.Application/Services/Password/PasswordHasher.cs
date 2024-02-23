using AuctionSite.DataAccess.Entities;

namespace AuctionSite.Application.Services.Password
{
    public class PasswordHasher : IPasswordHasher
    {
        public bool Decryption(string enteredPassword, string salt, string hashPassword)
        {
            var res = BCrypt.Net.BCrypt.Verify(enteredPassword, hashPassword);
            return res;
        }

        public (string salt,string hash) Encryption(string enteredPassword)
        {
            string SALT = BCrypt.Net.BCrypt.GenerateSalt();
            string HASH_PASSWORD = BCrypt.Net.BCrypt.HashPassword(enteredPassword, SALT);
            return (SALT, HASH_PASSWORD);
        }

    }
}
