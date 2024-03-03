using AuctionSite.Core.Contracts.Repositories.Specific;
using AuctionSite.DataAccess.Components.UpdateComponents;
using AuctionSite.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AuctionSite.DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services,string cntString)
        {
            services.AddDbContext<AuctionDbContext>(optionsAction =>
            {
                optionsAction.UseSqlServer(cntString);
            });

            services.AddScoped<IModifierArgumentChanger<AuctionDbContext>>(opt => 
            new ModifierArgumentChangerDecorator<AuctionDbContext>(new ModifierArgumentChanger<AuctionDbContext>()));

            services.AddScoped<ILotRepository, LotRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IBetRepository, BetRepository>();

            return services;
        }
    }
}
