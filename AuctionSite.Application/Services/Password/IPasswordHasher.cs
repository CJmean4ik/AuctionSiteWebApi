namespace AuctionSite.Application.Services.Password
{
    public interface IPasswordHasher
    {
        (string salt,string hash) Encryption(string enteredPassword);
        bool Decryption(string enteredPassword, string salt, string hashPassword);
    }
}
