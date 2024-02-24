using AuctionSite.Core.Contracts.Repositories.Specific;
using AuctionSite.Core.Models;
using AuctionSite.DataAccess.Entities;
using AuctionSite.DataAccess.Components.UpdateComponents;
using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;


namespace AuctionSite.DataAccess.Repositories
{
    public class LotRepository : ILotRepository
    {
        private readonly AuctionDbContext _dbContext;
        private readonly IModifierArgumentChanger<AuctionDbContext> _argumentChanger;
        private readonly IMapper _mapper;

        public LotRepository(AuctionDbContext dbContext,
                             IModifierArgumentChanger<AuctionDbContext> argumentChanger,
                             IMapper mapper)
        {
            _dbContext = dbContext;
            _argumentChanger = argumentChanger;
            _mapper = mapper;
        }

        public async Task SaveChangeAsync() => await _dbContext.SaveChangesAsync();
        public async Task<Result<List<Lot>>> ReadLimitAsync(int start, int limit)
        {
            if (start == 0 || limit == 0)
                return Result.Failure<List<Lot>>("Start page or limit there be shold be more 0");

            try
            {
                var lotsEntity = await _dbContext.Lots
                    .AsNoTracking()
                    .Skip((start - 1) * limit)
                    .Take(limit)
                    .ToListAsync();

                var lots = _mapper.Map<List<LotEntity>, List<Lot>>(lotsEntity);

                return Result.Success(lots);
            }
            catch (Exception ex)
            {
                return Result.Failure<List<Lot>>(ex.Message);
            }
        }
        public async Task<Result<string>> UpdateAsync(Lot newLot)
        {
            if (newLot is null)
                return Result.Failure<string>("There is no entity to update");

            try
            {
                var oldLotEntity = await _dbContext.Lots
                    .Where(w => w.Id == newLot.Id)
                    .FirstOrDefaultAsync();

                if (oldLotEntity is null)
                    return Result.Failure<string>($"The lot with this id: {newLot.Id} does not exist");

                var newLotEntity = _mapper.Map<Lot, LotEntity>(newLot);

                _argumentChanger.MarkedModifierProperty<LotEntity>(oldLotEntity, newLotEntity,_dbContext);

                await SaveChangeAsync();

                return Result.Success("The entity has been updated: " + newLot.Id);
            }
            catch (Exception ex)
            {
                return Result.Failure<string>(ex.Message);
            }
        }
        public async Task<Result<string>> AddAsync(SpecificLot entity)
        {
            try
            {
                var lotEntity = _mapper.Map<SpecificLot, SpecificLotEntity>(entity);

                await _dbContext.Lots.AddAsync(lotEntity);

                await SaveChangeAsync();

                return Result.Success("The entity has been added: " + lotEntity.Id);
            }
            catch (Exception ex)
            {
                return Result.Failure<string>(ex.Message);
            }
        }
        public async Task<Result<string>> DeleteAsync(int lotId)
        {
            try
            {
                var lotEntity = await _dbContext.Lots
                                          .Where(w => w.Id == lotId)
                                          .FirstOrDefaultAsync();

                if (lotEntity is null)
                    return Result.Failure<string>($"Entity by id {lotId} not found!");

                _dbContext.Lots.Remove(lotEntity);

                await SaveChangeAsync();

                return Result.Success("The entity has been removed: " + lotId);
            }
            catch (Exception ex)
            {
                return Result.Failure<string>(ex.Message);
            }
        }
        public async Task<Result<SpecificLot>> GetSpecificLotAsync(int id)
        {
            try
            {
                var lotConcreteEntity = await _dbContext.SpecificLot
                                                    .Include(s => s.Bets.OrderByDescending(b => b.Price).Take(5))!
                                                          .ThenInclude(b => b.Buyer)
                                                         .Include(s => s.Bets)!
                                                          .ThenInclude(b => b.Comments)
                                                           .ThenInclude(c => c.ReplyComments)
                                                    .Where(s => s.Id == id)
                                                    .FirstOrDefaultAsync();

                if (lotConcreteEntity is null)
                    return Result.Failure<SpecificLot>($"Concrete lot by id: {id} not found!");

                var lot = _mapper.Map<SpecificLotEntity, SpecificLot>(lotConcreteEntity);

                return Result.Success(lot);
            }
            catch (Exception ex)
            {
                return Result.Failure<SpecificLot>(ex.Message);
            }
        }
        public async Task<Result<string>> UpdateSpecificLotAsync(SpecificLot newLot)
        {
            if (newLot is null)
                return Result.Failure<string>("There is no entity to update");
            try
            {
                var oldLotEntity = await _dbContext.SpecificLot
                   .Where(w => w.Id == newLot.Lot!.Id)
                   .FirstOrDefaultAsync();

                if (oldLotEntity is null)
                    return Result.Failure<string>($"The lot with this id: {newLot.Lot.Id} does not exist");

                var newLotEntity = _mapper.Map<SpecificLot, SpecificLotEntity>(newLot);
                var property = _argumentChanger.MarkedModifierProperty<SpecificLotEntity>(oldLotEntity, newLotEntity, _dbContext);

                await SaveChangeAsync();

                return Result.Success("The entity has been updated: " + newLot.Lot.Id);
            }
            catch (Exception ex)
            {
                return Result.Failure<string>(ex.Message);
            }
        }

        public async Task<Result<List<Lot>>> GetAllUserLots(int buyerId, int start, int limit)
        {
            if (start == 0 || limit == 0)
                return Result.Failure<List<Lot>>("Start page or limit there be shold be more 0");

            try
            {
                var lotsEntity = await _dbContext.Lots
                    .AsNoTracking()
                    .Where(w => w.WhoCreatedUserId == buyerId)
                    .Skip((start - 1) * limit)
                    .Take(limit)
                    .ToListAsync();

                var lots = _mapper.Map<List<LotEntity>, List<Lot>>(lotsEntity);

                return Result.Success(lots);
            }
            catch (Exception ex)
            {
                return Result.Failure<List<Lot>>(ex.Message);
            }
        }
    }
}
