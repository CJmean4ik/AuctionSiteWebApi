using CSharpFunctionalExtensions;

namespace AuctionSite.Core.Contracts.Repositories
{
    public interface IDeleteRepository<T>
         where T : class
    {
        Task<Result> DeleteAsync(T entity);
    }
}