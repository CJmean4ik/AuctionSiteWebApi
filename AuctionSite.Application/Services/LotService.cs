using AuctionSite.Application.Services.Image;
using AuctionSite.Core.Contracts.Repositories.Specific;
using AuctionSite.Core.Models;
using CSharpFunctionalExtensions;

namespace AuctionSite.Application.Services
{
    public class LotService
    {
        private readonly ILotRepository _lotRepository;
        private readonly IImageService _imageService;
        public LotService(ILotRepository lotRepository)
        {
            _lotRepository = lotRepository;
        }

        public async Task<Result<string>> AddLotAsync(SpecificLot lot)
        {
            var result = await _lotRepository.AddAsync(lot);
            return result;
        }
        public async Task<Result<List<Lot>>> GetLotsAsync(int page = 1, int pageSize = 10)
        {
            var result = await _lotRepository.ReadLimitAsync(page, pageSize);
            return result;
        }
        public async Task<Result<string>> UpdateLotAsync(Lot lot)
        {
            var result = await _lotRepository.UpdateAsync(lot);
            return result;
        }
        public async Task<Result<string>> UpdateSpecificLotAsync(SpecificLot lot)
        {
            var result = await _lotRepository.UpdateSpecificLotAsync(lot);

            return result;
        }
        public async Task<Result<string>> RemoveLotAsync(int lotId)
        {
            var result = await _lotRepository.DeleteAsync(lotId);

            return result;
        }
        public async Task<Result<SpecificLot>> GetSpecificLotAsync(int id)
        {
            var result = await _lotRepository.GetSpecificLotAsync(id);

            return result;
        }

        public async Task<Result<List<Lot>>> GetUserLotsAsync(int buyerId,int start, int limit)
        {
            var result = await _lotRepository.GetAllUserLots(buyerId, start, limit);

            return result;
        }
    }
}