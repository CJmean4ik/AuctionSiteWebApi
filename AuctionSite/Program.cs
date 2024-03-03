using AuctionSite.Application;
using AuctionSite.Infrastructure;
using AuctionSite.DataAccess;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace AuctionSite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("AuctionDb")!;
            var azureConnectionString = builder.Configuration.GetConnectionString("AzureBlob")!;
            var typeEnviroment = builder.Configuration["TypeApplicationEnviroment"]!;


            builder.Services.AddSwaggerGen();
            builder.Services.AddControllers();
            builder.Services.AddCors();
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(option => option.LoginPath = "/login");
            builder.Services.AddAuthorization();

            builder.Services.AddApplication();
            builder.Services.AddDataAccess(connectionString);
            builder.Services.AddInfrastructure(azureConnectionString, typeEnviroment);

           var app = builder.Build();

            app.MapControllers();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger()
              .UseSwaggerUI(config =>
              {
                  config.RoutePrefix = string.Empty;
                  config.SwaggerEndpoint("/swagger/v1/swagger.json", "AUCTION REST API");
              });

            app.Run();
        }
    }
}
