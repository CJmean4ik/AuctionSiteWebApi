using AuctionSite.Core.Contracts.Repositories.Enitities;
using AuctionSite.Core.Models;
using Azure;
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

        public async Task<Result<string>> AddLotAsync(Lot lot)
        {
            var result = await _lotRepository.AddAsync(lot);

            if (result.IsFailure)
                return result;

            return Result.Success<string>(result.Value);
        }
        public async Task<Result<List<Lot>>> GetLotsAsync(int page = 1, int pageSize = 10)
        {
            var result = await _lotRepository.ReadLimitAsync(page,pageSize);

            if (result.IsFailure)
                return result;

            return Result.Success<List<Lot>>(result.Value);
        }
        public async Task<Result<string>> UpdateLotAsync(Lot lot)
        {
            var result = await _lotRepository.UpdateAsync(lot);

            if (result.IsFailure)
                return result;

            return Result.Success(result.Value);
        }
        public async Task<Result<string>> RemoveLotAsync(int lotId)
        {
            var result = await _lotRepository.DeleteAsync(lotId);

            if (result.IsFailure)
                return result;

            return Result.Success(result.Value);
        }

       
    }
}