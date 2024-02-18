using CSharpFunctionalExtensions;

namespace AuctionSite.Core.Contracts.Repositories
{
    public interface IDeleteRepository<T,R>
         where T : class
    {
        Task<Result<R>> DeleteAsync(int id);
    }
}