using AuctionSite.Core.Models;
using CSharpFunctionalExtensions;

namespace AuctionSite.Core.Contracts.Repositories.Specific
{
    public interface IBetRepository : ICreateRepository<Bet,string>
    {
        Task<Result<List<Bet>>> ReadLimitAsync(int start, int limit,int specificLotId);
    }
}
