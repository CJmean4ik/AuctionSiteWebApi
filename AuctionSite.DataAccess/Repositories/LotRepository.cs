using AuctionSite.Core.Contracts.Repositories.Enitities;
using AuctionSite.Core.Models;
using AuctionSite.DataAccess.Components.UpdateComponents;
using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace AuctionSite.DataAccess.Repositories
{
    public class LotRepository : ILotRepository
    {
        private readonly AuctionDbContext _dbContext;
        private readonly IModifierArgumentChanger<LotEntity, AuctionDbContext> _argumentChanger;
        private readonly IMapper _mapper;

        public LotRepository(AuctionDbContext dbContext,
                             IModifierArgumentChanger<LotEntity, AuctionDbContext> argumentChanger,
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
                    .Select(s => new LotEntity
                    {
                        Id = s.Id,
                        Name = s.Name,
                        CategoryName = s.CategoryName,
                        ShortDescription = s.ShortDescription
                    })
                    .FirstOrDefaultAsync();

                if (oldLotEntity is null)
                    return Result.Failure<string>($"The lot with this id: {newLot.Id} does not exist");

                var newLotEntity = _mapper.Map<Lot, LotEntity>(newLot);

                _argumentChanger.SearchModifierProperty(oldLotEntity, newLotEntity);
                _argumentChanger.ChangeAndAttachValue(_dbContext, oldLotEntity);

                await SaveChangeAsync();

                return Result.Success("The entity has been updated: " + newLot.Id);
            }
            catch (Exception ex)
            {
                return Result.Failure<string>(ex.Message);
            }
        }
        public async Task<Result<string>> AddAsync(Lot entity)
        {
            try
            {
                var lotEntity = _mapper.Map<Lot, LotEntity>(entity);

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
    }
}
