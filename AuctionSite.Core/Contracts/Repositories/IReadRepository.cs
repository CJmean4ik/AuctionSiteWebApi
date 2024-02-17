using CSharpFunctionalExtensions;

namespace AuctionSite.Core.Contracts.Repositories
{
    public interface IReadRepository<R>
    {
        Task<Result<List<R>>> ReadLimitAsync(int start,int limit);
    }
}