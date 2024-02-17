using CSharpFunctionalExtensions;

namespace AuctionSite.Core.Contracts.Repositories
{
    public interface IUpdateRepository<T>
                where T : class
    {
        Task<Result> UpdateAsync(T entity);
    }
}