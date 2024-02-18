using AuctionSite.Core.Models;
using CSharpFunctionalExtensions;

namespace AuctionSite.Core.Contracts.Repositories.Enitities
{
    public interface ILotRepository : IRepository<Lot,string>
    {
        Task SaveChangeAsync();
    }
}
