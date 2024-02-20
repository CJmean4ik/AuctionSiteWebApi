using AuctionSite.Core.Models;
namespace AuctionSite.Core.Contracts.Repositories.Concrete
{
    public interface ILotRepository : IRepository<Lot,string>
    {
        Task SaveChangeAsync();
    }
}
