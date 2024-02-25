using AuctionSite.Core.Contracts.Repositories.Specific;
using AuctionSite.Core.Models;
using CSharpFunctionalExtensions;

namespace AuctionSite.Application.Services
{
    public class CommentsService
    {
        private ICommentRepository _commentRepository;

        public CommentsService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<Result<string>> AddCommentAsync(ReplyComments replyComments)
        {
            var result = await _commentRepository.AddAsync(replyComments);
            return result;
        }
    }
}
