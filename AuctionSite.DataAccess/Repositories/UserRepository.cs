using AuctionSite.Core.Contracts.Repositories.Specific;
using AuctionSite.Core.Models;
using AuctionSite.DataAccess.Entities;
using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;


namespace AuctionSite.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuctionDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserRepository(AuctionDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<string>> AddAsync(Buyer buyer)
        {
            try
            {
                var userEntity = await _dbContext.Buyers.Where(w => w.Email == buyer.User.Email).FirstOrDefaultAsync();

                if (userEntity is not null)
                    return Result.Failure<string>($"User with that email: {buyer.User.Email}  already exist");

                userEntity = _mapper.Map<BuyerEntity>(buyer);

                await _dbContext.Users.AddAsync(userEntity);

                await _dbContext.SaveChangesAsync();

                return Result.Success("The user has been successfully registered");
            }
            catch (Exception ex)
            {
                return Result.Failure<string>(ex.Message);
            }
        }
        public async Task<Result<Buyer>> GetByEmail(string email)
        {
            try
            {
                var userEntity = await _dbContext.Buyers.Where(w => w.Email == email).FirstOrDefaultAsync();

                if (userEntity is null)
                    return Result.Failure<Buyer>($"User by email: {email} not founded");

                var user = _mapper.Map<Buyer>(userEntity);
                return Result.Success(user);
            }
            catch (Exception ex)
            {
                return Result.Failure<Buyer>(ex.Message);
            }
        }

    }
}
