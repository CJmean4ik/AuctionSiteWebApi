using Azure.Storage.Blobs;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;

namespace AuctionSite.Application.Services.Image
{
    public class BlobImageService : IImageService
    {
        private readonly BlobServiceClient _serviceClient;

        public BlobImageService(BlobServiceClient serviceClient)
        {
            _serviceClient = serviceClient;
        }

        public async Task<Result<Stream>> ReadImageAsync(string fileName)
        {
            try
            {
                var blobClient = GetBlobClient("previewimage", fileName);

                if (!await blobClient.ExistsAsync())
                    return Result.Failure<Stream>($"Image blob by name: {fileName} not found!");

                var blobResult = await blobClient.DownloadStreamingAsync();

                return Result.Success(blobResult.Value.Content);
            }
            catch (Exception ex)
            {
                return Result.Failure<Stream>(ex.Message);
            }
        }
        public async Task<Result<string>> CreateImageAsync(IFormFile formFile)
        {
            try
            {
                var blobClient = await GetBlobClient("previewimage", formFile,true);

                Stream stream = new MemoryStream();
                await formFile.CopyToAsync(stream);
                stream.Position = 0;

                await blobClient.UploadAsync(stream);

                return Result.Success(blobClient.Name);
            }
            catch (Exception ex)
            {
                return Result.Failure<string>(ex.Message);
            }
        }
        public async Task<Result<string>> UpdateAsync(IFormFile newImage, string oldImage)
        {
            var blobClient =  GetBlobClient("previewimage", oldImage);

            try
            {
                if (!await blobClient.DeleteIfExistsAsync())
                    return Result.Failure<string>($"Blob file by image: {oldImage} doesnt exist!");

                Stream stream = new MemoryStream();
                await newImage.CopyToAsync(stream);
                stream.Position = 0;
                await blobClient.UploadAsync(stream);

                return Result.Success($"Image by name {oldImage} has been updated");
            }
            catch (Exception ex)
            {
                return Result.Failure<string>($"Failed to update blob file in Azure: {ex.Message}");
            }
        }
        public async Task<Result<string>> DeleteAsync(string oldImage)
        {
            try
            {
                var blobClient = GetBlobClient("previewimage", oldImage);

                if (!await blobClient.DeleteIfExistsAsync())
                    return Result.Failure<string>($"Blob file by name: {oldImage} doesnt exist!");

                return Result.Success($"Blob file by name: {oldImage} has been deleted");
            }
            catch (Exception ex)
            {
                return Result.Failure<string>($"Failed to delete blob file in Azure: {ex.Message}");
            }
        }
        private async Task<BlobClient> GetBlobClient(string containerName,IFormFile? formFile, bool isCreate = false)
        {
            var blobClient = GetBlobClient(containerName, formFile.FileName);

            if (isCreate && await blobClient.ExistsAsync())
            {
                int index = new Random().Next(1, 10000);
                string[] splitFileName = formFile.FileName.Split(".");
                string newName = $"{splitFileName[0]}{index}.{splitFileName[1]}";
                blobClient = GetBlobClient(containerName, newName);
                return blobClient;
            }

            return blobClient;
        }
        private BlobClient GetBlobClient(string containerName, string? formFile)
        {
            var clientContainer = _serviceClient.GetBlobContainerClient(containerName);
            var blobClient = clientContainer.GetBlobClient(formFile);
            return blobClient;
        }
    }
}
