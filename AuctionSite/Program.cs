using AuctionSite.API.Contracts;
using AuctionSite.API.Services.ErrorValidation;
using AuctionSite.Application.Services;
using AuctionSite.Application.Services.Image;
using AuctionSite.Core.Contracts.Repositories.Concrete;
using AuctionSite.Core.Models;
using AuctionSite.DataAccess;
using AuctionSite.DataAccess.Components.UpdateComponents;
using AuctionSite.DataAccess.Repositories;
using Azure.Storage.Blobs;
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

            builder.Services.AddSingleton(x => new BlobServiceClient(builder.Configuration.GetConnectionString("AzureBlob")));

            builder.Services.AddScoped<IErrorValidationHandler<List<ErrorModel>, ModelStateDictionary>, ErrorValidationHandler>();
            builder.Services.AddScoped<IModifierArgumentChanger<LotEntity, AuctionDbContext>, ModifierArgumentChanger<LotEntity, AuctionDbContext>>();
            builder.Services.AddScoped<ILotRepository, LotRepository>();

            var typeEnviroment = builder.Configuration["TypeApplicationEnviroment"];

            if (typeEnviroment == "Local")           
                builder.Services.AddScoped<IImageService, LocalImageService>();
            
            if (typeEnviroment == "AzureBlob")           
                builder.Services.AddScoped<IImageService, BlobImageService>();
                    
            builder.Services.AddScoped<LotService>();

            var app = builder.Build();

            app.MapControllers();
            app.UseCors();

            app.Run();
        }
    }
}
