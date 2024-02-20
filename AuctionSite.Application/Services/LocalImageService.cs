using AuctionSite.Application.Model;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuctionSite.Application.Services
{
    public class LocalImageService : IImageService
    {
        public async Task<Result<string>> SaveImageAsync(IFormFile formFile, ImageType imageType)
        {
            try
            {
                var contentPath = CombinePathByImageType(imageType) + formFile.FileName;

                using (FileStream file = new FileStream(path: contentPath, FileMode.CreateNew))
                    await formFile.CopyToAsync(file);

                return Result.Success("Image has been saved. Name:  " + formFile.FileName);
            }
            catch (Exception ex)
            {
                return Result.Failure<string>(ex.Message);
            }
        }
        public async Task<Result<T>> GetImage<T>(string fileName, ImageType imageType) where T : class
        {
            string currentPath = CombinePathByImageType(imageType);
            string imagePath = Path.Combine(currentPath, fileName);

            if (!File.Exists(imagePath))
                return Result.Failure<T>($"Image by path: {imagePath} not found!");

            if (typeof(T) != typeof(PhysicalFileResult))
                return Result.Failure<T>($"The specified {typeof(T)} type for the T parameter is not valid for this method");

            return Result.Success((T)(object)new PhysicalFileResult(imagePath, "image/jpeg"));
        }
        public Result<string> Delete(string fileName, ImageType imageType)
        {
            string currentPath = CombinePathByImageType(imageType);
            string imagePath = Path.Combine(currentPath, fileName);

            if (!File.Exists(imagePath))
                return Result.Failure<string>($"Image by path: {imagePath} not found!");

            File.Delete(imagePath);
            return Result.Success($"Image {fileName} has been deleted!");
        }

        private string CombinePathByImageType(ImageType imageType) => imageType switch
        {
            ImageType.PreviewImage => "C:\\ImageCatalog\\PreviewImage",
            ImageType.FullImage => "C:\\ImageCatalog\\FullImage",
            _ => "",
        };

    }
}
