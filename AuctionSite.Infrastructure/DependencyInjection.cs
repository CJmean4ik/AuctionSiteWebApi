using AuctionSite.Infrastructure.Image;
using AuctionSite.Infrastructure.Password;
using Azure.Storage.Blobs;
using Microsoft.Extensions.DependencyInjection;

namespace AuctionSite.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,string azureCntString,string enviromentType)
        {
            services.AddSingleton(x => new BlobServiceClient(azureCntString));

            if (enviromentType == "Local")
                services.AddScoped<IImageService, LocalImageService>();

            if (enviromentType == "AzureBlob")
                services.AddScoped<IImageService, BlobImageService>();

            services.AddScoped<IPasswordHasher, PasswordHasher>();

            return services;
        }
    }
}
