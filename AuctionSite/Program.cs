using AuctionSite.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace AuctionSite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddDbContext<AuctionDbContext>(optionsAction => 
            {
                optionsAction.UseSqlServer(builder.Configuration.GetConnectionString("AuctionDb"));
            });

            var app = builder.Build();

            app.MapControllers();

            app.Run();
        }
    }
}
