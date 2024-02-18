using CSharpFunctionalExtensions;

namespace AuctionSite.Core.Contracts.Repositories
{
    public interface IUpdateRepository<T, R>
                where T : class
    {
        Task<Result<R>> UpdateAsync(T entity);
    }
}