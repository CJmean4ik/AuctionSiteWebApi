using AuctionSite.Core.Models;
using CSharpFunctionalExtensions;

namespace AuctionSite.Core.Contracts.Repositories.Specific
{
    public interface ILotRepository : 
        IReadRepository<Lot>,
        ICreateRepository<SpecificLot,string>,
        IDeleteRepository<Lot,string>,
        IUpdateRepository<Lot,string>
    {
        Task<Result<SpecificLot>> GetSpecificLotAsync(int id);
        Task<Result<string>> UpdateSpecificLotAsync(SpecificLot specificLot);
        Task<Result<List<Lot>>> GetAllUserLots(int buyerId, int start, int limit);
        Task SaveChangeAsync();
    }
}
