using AuctionSite.Core.Models;
using CSharpFunctionalExtensions;

namespace AuctionSite.Core.Contracts.Repositories.Concrete
{
    public interface ILotRepository : IRepository<Lot,string>
    {
        Task<Result<SpecificLot>> GetSpecificLotAsync(int id);
        Task<Result<string>> UpdateSpecificLotAsync(SpecificLot specificLot);
        Task SaveChangeAsync();
    }
}
