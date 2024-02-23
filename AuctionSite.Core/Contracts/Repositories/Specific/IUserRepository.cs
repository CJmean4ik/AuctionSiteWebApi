using AuctionSite.Core.Models;
using CSharpFunctionalExtensions;

namespace AuctionSite.Core.Contracts.Repositories.Specific
{
    public interface IUserRepository : ICreateRepository<Buyer,string>
    {
        Task<Result<Buyer>> GetByEmail(string email);
    }
}
