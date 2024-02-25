using AuctionSite.Core.Contracts.Repositories.Specific;
using AuctionSite.Core.Models;
using AuctionSite.DataAccess.Entities;
using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace AuctionSite.DataAccess.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AuctionDbContext _auctionDb;
        private readonly IMapper _mapper;
        
        
        public CommentRepository(AuctionDbContext auctionDb, IMapper mapper)
        {
            _auctionDb = auctionDb;
            _mapper = mapper;
        }

        public async Task<Result<string>> AddAsync(ReplyComments entity)
        {
            try
            {
                var bet = await _auctionDb.Bets
                    .Where(w => w.Id == entity.BetId)
                    .Include(i => i.ReplyComments)
                    .FirstOrDefaultAsync();

                if (bet == null)               
                    return Result.Failure<string>($"Bet by id: {entity.BetId} not found!");

                var replyComment = _mapper.Map<ReplyCommentsEntity>(entity);

                bet.ReplyComments!.Add(replyComment);

                await _auctionDb.SaveChangesAsync();

                return Result.Success("Comment has been added!");
            }
            catch (Exception ex)
            {
                return Result.Failure<string>(ex.Message);
            }
        }
    }
}
