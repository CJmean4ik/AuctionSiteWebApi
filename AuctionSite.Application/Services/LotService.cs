using AuctionSite.Application.DTO;
using AuctionSite.Core.Contracts.Repositories;
using AuctionSite.Core.Contracts.Repositories.Enitities;
using AuctionSite.Core.Models;
using CSharpFunctionalExtensions;

namespace AuctionSite.Application.Services
{
    public class LotService
    {
        private readonly ILotRepository _lotRepository;

        public LotService(ILotRepository lotRepository)
        {
            _lotRepository = lotRepository;
        }

        public async Task<Result<List<Lot>>> GetLotsAsync(int page = 1, int pageSize = 10)
        {
            var result = await _lotRepository.ReadLimitAsync(page,pageSize);

            if (result.IsFailure)
                return result;

            return Result.Success<List<Lot>>(result.Value);
        }

        public Task<Result<string>> UpdateLotAsync(UpdateLotDto updateLotDto)
        {
            throw new NotImplementedException();
        }
    }
}