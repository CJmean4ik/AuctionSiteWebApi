using CSharpFunctionalExtensions;

namespace AuctionSite.Core.Contracts.Repositories
{
    public interface ICreateRepository<T>
        where T : class
    {
        Task<Result> AddAsync(T entity);
    }
}