using AuctionSite.Application.Services;
using AuctionSite.Core.Contracts.Repositories.Enitities;
using AuctionSite.DataAccess;
using AuctionSite.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AuctionSite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddCors();

            builder.Services.AddDbContext<AuctionDbContext>(optionsAction =>
            {
                optionsAction.UseSqlServer(builder.Configuration.GetConnectionString("AuctionDb"));
            });

            builder.Services.AddScoped<ILotRepository, LotRepository>();
            builder.Services.AddScoped<LotService>();

            var app = builder.Build();

            app.MapControllers();
            app.UseCors();

            app.Run();
        }
    }
}
