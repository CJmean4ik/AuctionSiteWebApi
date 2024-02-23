using AuctionSite.Core.Contracts.Repositories.Specific;
using AuctionSite.Core.Models;
using AuctionSite.DataAccess.Entities;
using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace AuctionSite.DataAccess.Repositories
{
    public class BetRepository : IBetRepository
    {
        private readonly AuctionDbContext _dbContext;
        private readonly IMapper _mapper;

        public BetRepository(AuctionDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public Task<Result<string>> AddAsync(Bet entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<List<Bet>>> ReadLimitAsync(int start, int limit, int specificLotId)
        {
            var bets = new List<Bet>();
            if (start == 0 || limit == 0)
                return Result.Failure<List<Bet>>("Start page or limit there be shold be more 0");

            try
            {
                if (await _dbContext.Lots.FindAsync(specificLotId) == null)              
                    return Result.Failure<List<Bet>>($"Lot by id: {specificLotId} not found");
                

                var betsEntity = await _dbContext.Bets
                    .AsNoTracking()
                    .Where(w => w.LotId == specificLotId)
                    .Include(i => i.Buyer)
                    .Include(i => i.Comments)
                        .ThenInclude(ti => ti!.ReplyComments)
                    .Skip((start - 1) * limit)
                    .Take(limit)
                    .ToListAsync();

                foreach (var betEntity in betsEntity)
                {
                    var bet = MapBet(betEntity);
                    bets.Add(bet);
                }

                return Result.Success(bets);
            }
            catch (Exception ex)
            {
                return Result.Failure<List<Bet>>(ex.Message);
            }
        }
        private Bet? MapBet(BetEntity? betEntity)
        {
            if (betEntity == null)
                return null;

            var buyer = betEntity.Buyer;
            if (buyer == null)
                return null;

            string comments = betEntity.Comments?.Text;

            var bet = Bet.Create(betEntity.Price, buyer.FirstName, buyer.SecondName, comments, betEntity.Id).Value;

            if (betEntity.Comments != null && betEntity.Comments.ReplyComments != null)
            {
                var replyComments = betEntity.Comments.ReplyComments.Select(c =>
                    ReplyComments.Create(c.Text, c.UserName).Value).ToList();
                bet.AddReplyComments(replyComments);
            }

            return bet;
        }
    }
}
