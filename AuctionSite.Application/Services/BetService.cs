using AuctionSite.Core.Contracts.Repositories.Specific;
using AuctionSite.Core.Models;
using CSharpFunctionalExtensions;

namespace AuctionSite.Application.Services
{
    public class BetService
    {
        private IBetRepository _betRepository;

        public BetService(IBetRepository betRepository)
        {
            _betRepository = betRepository;
        }

        public async Task<Result<List<Bet>>> GetBets(int page,int count,int lotId)
        {
            var result = await _betRepository.ReadLimitAsync(page,count,lotId);



            return result;
        }
          
    }
}
