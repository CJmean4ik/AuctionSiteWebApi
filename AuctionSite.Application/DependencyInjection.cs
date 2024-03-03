using AuctionSite.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AuctionSite.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            services.AddScoped<LotService>();
            services.AddScoped<BetService>();
            services.AddScoped<UserService>();
            services.AddScoped<CommentsService>();

            return services;
        }
    }
}
