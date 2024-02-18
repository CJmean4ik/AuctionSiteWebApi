using AuctionSite.API.Contracts;
using AuctionSite.API.Services.ErrorValidation;
using AuctionSite.Application.Services;
using AuctionSite.Core.Contracts.Repositories.Enitities;
using AuctionSite.Core.Models;
using AuctionSite.DataAccess;
using AuctionSite.DataAccess.Components.UpdateComponents;
using AuctionSite.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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

            builder.Services.AddDbContext<AuctionDbContext>(optionsAction =>
            {
                optionsAction.UseSqlServer(builder.Configuration.GetConnectionString("AuctionDb"));
            });

            builder.Services.AddScoped<IErrorValidationHandler<List<ErrorModel>, ModelStateDictionary>, ErrorValidationHandler>();
            builder.Services.AddScoped<IModifierArgumentChanger<LotEntity, AuctionDbContext>, ModifierArgumentChanger<LotEntity, AuctionDbContext>>();           
            builder.Services.AddScoped<ILotRepository, LotRepository>();

            builder.Services.AddScoped<LotService>();
            builder.Services.AddScoped<ImageService>();

            var app = builder.Build();

            app.MapControllers();
            app.UseCors();

            app.Run();
        }
    }
}
