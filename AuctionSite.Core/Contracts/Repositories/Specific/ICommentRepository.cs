using AuctionSite.Core.Models;

namespace AuctionSite.Core.Contracts.Repositories.Specific
{
    public interface ICommentRepository : ICreateRepository<ReplyComments,string>
    {
    }
}
