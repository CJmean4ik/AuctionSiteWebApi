using AuctionSite.Core.Contracts.Repositories.Enitities;
using AuctionSite.Core.Models;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace AuctionSite.DataAccess.Repositories
{
    public class LotRepository : ILotRepository
    {
        private readonly AuctionDbContext _dbContext;
        public LotRepository(AuctionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Result> AddAsync(Lot entity)
        {
            throw new NotImplementedException();
        }

        public Task<Result> DeleteAsync(Lot entity)
        {
            throw new NotImplementedException();
        }

        public Task<Result> GetConcreteLotAsync(int id)
        {
            throw new NotImplementedException();
        }

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

                var lots = lotsEntity.Select(s => Lot.Create(s.Name, s.ShortDescription, s.CategoryName,
                                                  Image.Create(s.ImagePreview, 5000).Value, s.Id).Value).ToList();

                return Result.Success<List<Lot>>(lots);
            }
            catch (Exception ex)
            {
                return Result.Failure<List<Lot>>(ex.Message);
            }
        }

        public Task<Result> UpdateAsync(Lot entity)
        {
            throw new NotImplementedException();
        }
    }
}
