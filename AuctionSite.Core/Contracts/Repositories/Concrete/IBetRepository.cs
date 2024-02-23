using AuctionSite.Core.Models;
using CSharpFunctionalExtensions;

namespace AuctionSite.Core.Contracts.Repositories.Concrete
{
    public interface IBetRepository : ICreateRepository<Bet,string>
    {
        Task<Result<List<string>>> ReadLimitAsync(int start, int limit,int specificLotId);
    }
}
