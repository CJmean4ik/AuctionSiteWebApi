using AuctionSite.Application;
using AuctionSite.Application.Services;
using AuctionSite.Application.Services.Image;
using AuctionSite.Application.Services.Password;
using AuctionSite.Core.Contracts.Repositories.Specific;
using AuctionSite.DataAccess;
using AuctionSite.DataAccess.Components.UpdateComponents;
using AuctionSite.DataAccess.Repositories;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authentication.Cookies;
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
            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(option => option.LoginPath = "/login");
            builder.Services.AddAuthorization();

            builder.Services.AddDbContext<AuctionDbContext>(optionsAction =>
            {
                optionsAction.UseSqlServer(builder.Configuration.GetConnectionString("AuctionDb"));
            });

            builder.Services.AddSingleton(x => new BlobServiceClient(builder.Configuration.GetConnectionString("AzureBlob")));

            builder.Services.AddScoped<IModifierArgumentChanger<AuctionDbContext>>(opt => new ModifierArgumentChangerDecorator<AuctionDbContext>(new ModifierArgumentChanger<AuctionDbContext>()));
            builder.Services.AddScoped<ILotRepository, LotRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IBetRepository, BetRepository>();
            builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

            var typeEnviroment = builder.Configuration["TypeApplicationEnviroment"];

            if (typeEnviroment == "Local")           
                builder.Services.AddScoped<IImageService, LocalImageService>();
            
            if (typeEnviroment == "AzureBlob")           
                builder.Services.AddScoped<IImageService, BlobImageService>();
                    
            builder.Services.AddScoped<LotService>();
            builder.Services.AddScoped<BetService>();
            builder.Services.AddScoped<UserService>();

            var app = builder.Build();

            app.MapControllers();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();

            app.Run();
        }
    }
}
