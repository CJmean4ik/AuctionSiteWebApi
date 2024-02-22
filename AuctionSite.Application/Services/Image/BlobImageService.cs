﻿using AuctionSite.Application.Model;
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

        public async Task<Result<Stream>> ReadImageAsync(string fileName, ImageType imageType)
        {
            try
            {
                var blobClient = GetBlobClient(imageType, fileName);

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
        public async Task<Result<string>> CreateImageAsync(IFormFile formFile, ImageType imageType)
        {
            try
            {
                var blobClient = GetBlobClient(imageType, formFile.FileName);

                Stream stream = new MemoryStream();
                await formFile.CopyToAsync(stream);
                stream.Position = 0;

                await blobClient.UploadAsync(stream);

                return Result.Success($"Image by name {formFile.FileName} has been saved");
            }
            catch (Exception ex)
            {
                return Result.Failure<string>(ex.Message);
            }
        }
        public async Task<Result<string>> UpdateAsync(IFormFile newImage, string oldImage, ImageType imageType)
        {
            var blobClient = GetBlobClient(imageType, oldImage);

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
        public async Task<Result<string>> DeleteAsync(string oldImage, ImageType imageType)
        {
            try
            {
                var blobClient = GetBlobClient(imageType, oldImage);

                if (!await blobClient.DeleteIfExistsAsync())
                    return Result.Failure<string>($"Blob file by name: {oldImage} doesnt exist!");

                return Result.Success($"Blob file by name: {oldImage} has been deleted");
            }
            catch (Exception ex)
            {
                return Result.Failure<string>($"Failed to delete blob file in Azure: {ex.Message}");
            }
        }

        private BlobClient GetBlobClient(ImageType imageType, string fileName)
        {
            string containerName = CombinePathByImageType(imageType);
            var clientContainer = _serviceClient.GetBlobContainerClient(containerName);
            var blobClient = clientContainer.GetBlobClient(fileName);



            return blobClient;
        }
        private string CombinePathByImageType(ImageType imageType) => imageType switch
        {
            ImageType.PreviewImage => "previewimage",
            ImageType.FullImage => "fullimage",
            _ => "",
        };

    }
}