using AuctionSite.Core.Models;
using CSharpFunctionalExtensions;

namespace AuctionSite.Core.Contracts.Repositories.Concrete
{
    public interface ILotRepository : 
        IReadRepository<Lot>,
        ICreateRepository<SpecificLot,string>,
        IDeleteRepository<Lot,string>,
        IUpdateRepository<Lot,string>
    {
        Task<Result<SpecificLot>> GetSpecificLotAsync(int id);
        Task<Result<string>> UpdateSpecificLotAsync(SpecificLot specificLot);
        Task SaveChangeAsync();
    }
}
