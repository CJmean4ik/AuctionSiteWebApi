using AuctionSite.Core.Contracts.Repositories.Specific;
using AuctionSite.Core.Models;
using AuctionSite.DataAccess.Entities;
using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

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

        public async Task<Result<string>> AddAsync(Bet entity)
        {
            try
            {
                var lot = await _dbContext.SpecificLot.FindAsync(entity.LotId);

                if (lot == null)
                    return Result.Failure<string>($"Lot by id: {entity.LotId} not found");

                var bets = await _dbContext.Bets
                                           .AsNoTracking()
                                           .Where(w => w.LotId == entity.LotId && w.Price > entity.Price)
                                           .ToListAsync();

                if (bets.Count == 0)
                {
                    lot.MaxPrice = entity.Price;
                    _dbContext.Entry(lot).Property(p => p.MaxPrice).IsModified = true;
                }

                var betEntity = new BetEntity 
                {
                    Price = entity.Price,
                    BuyerId = entity.UserId,
                    LotId = lot.Id,
                    Comments = entity.Comments
                };

                await _dbContext.Bets.AddAsync(betEntity);
                await _dbContext.SaveChangesAsync();

                return Result.Success<string>("Bet has been added");
            }
            catch (Exception ex)
            { 
                return Result.Failure<string>(ex.Message);
            }
        }
        public async Task<Result<List<Bet>>> GetAllBuyerBets(int buyerId, int start, int limit)
        {
            var bets = new List<Bet>();
            try
            {       
                var betsEntity = await _dbContext.Bets
                    .AsNoTracking()
                    .Where(w => w.BuyerId == buyerId)
                    .Include(i => i.Buyer)
                    .Include(i => i.ReplyComments)
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
        public async Task<Result<Bet>> GetMaxBet(int lotId)
        {
            try
            {
                var lot = await _dbContext.SpecificLot.FindAsync(lotId);

                if (lot == null)
                    return Result.Failure<Bet>($"Lot by id: {lotId} not found");


                var betEntity = await _dbContext.Bets
                    .AsNoTracking()
                    .Where(w => w.LotId == lotId && w.Price == lot.MaxPrice)
                    .Include(i => i.Buyer)
                     .Include(i => i.ReplyComments)
                    .FirstOrDefaultAsync();

                var bet = _mapper.Map<Bet>(betEntity);

                return Result.Success(bet);
            }
            catch (Exception ex)
            {
                return Result.Failure<Bet>(ex.Message);
            }
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
                    .Include(i => i.ReplyComments)
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

            string comments = betEntity.Comments;

            var bet = Bet.Create(buyer.FirstName, buyer.SecondName, comments, betEntity.Price, betEntity.Id).Value;

            if (betEntity.Comments != null && betEntity.ReplyComments != null)
            {
                var replyComments = betEntity.ReplyComments.Select(c =>
                    ReplyComments.Create(c.Text, c.UserName).Value).ToList();
                bet.AddReplyComments(replyComments);
            }

            return bet;
        }
    }
}
