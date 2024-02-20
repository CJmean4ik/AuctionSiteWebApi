using AuctionSite.Application.Model;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace AuctionSite.Application.Services
{
    public class BlobImageService : IImageService
    {
        private readonly BlobServiceClient _serviceClient;

        public BlobImageService(BlobServiceClient serviceClient)
        {
            _serviceClient = serviceClient;
        }

        public Result<string> Delete(string fileName, ImageType imageType)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<T>> GetImage<T>(string fileName, ImageType imageType) where T : class
        {
            try
            {
                var containerName = CombinePathByImageType(imageType);
                var clientContainer = _serviceClient.GetBlobContainerClient(containerName);
                var blobClient = clientContainer.GetBlobClient(fileName);

                if (!await blobClient.ExistsAsync())
                    return Result.Failure<T>($"Image blob by name: {fileName} not found!");

                var blobResult = await blobClient.DownloadStreamingAsync();

                if (typeof(T) != typeof(Stream))        
                    return Result.Failure<T>($"The specified {typeof(T)} type for the T parameter is not valid for this method");
                
                return Result.Success((T)(object)blobResult.Value.Content);
            }
            catch (Exception ex)
            {
                return Result.Failure<T>(ex.Message);
            }
        }

        public Task<Result<string>> SaveImageAsync(IFormFile formFile, ImageType imageType)
        {
            throw new NotImplementedException();
        }

        private string CombinePathByImageType(ImageType imageType) => imageType switch
        {
            ImageType.PreviewImage => "previewimage",
            ImageType.FullImage => "fullimage",
            _ => "",
        };
    }
}
