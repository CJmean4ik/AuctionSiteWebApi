namespace AuctionSite.Core.Contracts.Repositories
{
    public interface IRepository<T> :
        ICreateRepository<T>,
        IUpdateRepository<T>,
        IReadRepository<T>,
        IDeleteRepository<T>
        where T : class
    {

    }
}
