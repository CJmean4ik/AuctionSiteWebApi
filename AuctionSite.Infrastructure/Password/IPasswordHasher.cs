namespace AuctionSite.Infrastructure.Password
{
    public interface IPasswordHasher
    {
        (string salt,string hash) Encryption(string enteredPassword);
        bool Decryption(string enteredPassword, string salt, string hashPassword);
    }
}
