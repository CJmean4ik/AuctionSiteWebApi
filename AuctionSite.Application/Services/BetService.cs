using AuctionSite.Core.Contracts.Repositories.Specific;
using AuctionSite.Core.Models;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;

namespace AuctionSite.Application.Services
{
    public class BetService
    {
        private IBetRepository _betRepository;

        public BetService(IBetRepository betRepository)
        {
            _betRepository = betRepository;
        }

        public async Task<Result<string>> CreateBet(Bet bet)
        {
            var result = await _betRepository.AddAsync(bet);
            return result;
        }
        public async Task<Result<List<Bet>>> GetBets(int page,int count,int lotId)
        {
            var result = await _betRepository.ReadLimitAsync(page,count,lotId);
            return result;
        }
        public async Task<Result<List<Bet>>> GetBueyrBets(int buyerId, int page = 1, int limit = 10)
        {
            var result = await _betRepository.GetAllBuyerBets(buyerId, page, limit);
            return result;
        }
        public async Task<Result<Bet>> GetMaxBet(int lotId)
        {
            var result = await _betRepository.GetMaxBet(lotId);
            return result;
        }
    }
}
