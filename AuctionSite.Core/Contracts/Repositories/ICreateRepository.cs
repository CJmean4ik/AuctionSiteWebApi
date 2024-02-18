using CSharpFunctionalExtensions;

namespace AuctionSite.Core.Contracts.Repositories
{
    public interface ICreateRepository<T,R>
        where T : class
    {
        Task<Result<R>> AddAsync(T entity);
    }
}