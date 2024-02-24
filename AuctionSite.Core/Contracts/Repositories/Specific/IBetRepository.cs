using AuctionSite.Core.Models;
using CSharpFunctionalExtensions;

namespace AuctionSite.Core.Contracts.Repositories.Specific
{
    public interface IBetRepository : ICreateRepository<Bet,string>
    {
        Task<Result<List<Bet>>> ReadLimitAsync(int start, int limit,int specificLotId);
        Task<Result<Bet>> GetMaxBet(int lotId);
        Task<Result<List<Bet>>> GetAllBuyerBets(int buyerId, int start,int limit);
    }
}
