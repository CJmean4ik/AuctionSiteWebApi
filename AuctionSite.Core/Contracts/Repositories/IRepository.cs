namespace AuctionSite.Core.Contracts.Repositories
{
    public interface IRepository<T,R> :
        ICreateRepository<T,R>,
        IUpdateRepository<T,R>,
        IDeleteRepository<T,R>,
        IReadRepository<T>
        where T : class
    {

    }
}
