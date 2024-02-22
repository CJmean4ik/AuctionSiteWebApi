using AuctionSite.Application.Model;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;

namespace AuctionSite.Application.Services
{
    public class LocalImageService : IImageService
    {
        public async Task<Result<string>> CreateImageAsync(IFormFile formFile, ImageType imageType)
        {
            try
            {
                var directory = CombinePathByImageType(imageType);
                var fullPath = directory + formFile.FileName;

                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                using (FileStream file = new FileStream(path: fullPath, FileMode.CreateNew))
                    await formFile.CopyToAsync(file);

                return Result.Success("Image has been saved. Name:  " + formFile.FileName);
            }
            catch (Exception ex)
            {
                return Result.Failure<string>(ex.Message);
            }
        }
        public async Task<Result<string>> DeleteAsync(string oldImage, ImageType imageType)
        {
            string directory = CombinePathByImageType(imageType);
            string imagePathOld = Path.Combine(directory, oldImage);

            if (!File.Exists(imagePathOld))
                return Result.Failure<string>($"Image for update by path: {imagePathOld} not found!");

            await Task.Run(() => File.Delete(imagePathOld));

            return Result.Success($"Image {imagePathOld} has been deleted!");
        }
        public async Task<Result<Stream>> ReadImageAsync(string fileName, ImageType imageType)
        {
            string currentPath = CombinePathByImageType(imageType);
            string imagePath = Path.Combine(currentPath, fileName);

            if (!File.Exists(imagePath))
                return Result.Failure<Stream>($"Image by path: {imagePath} not found!");

            try
            {
                FileStream fileStream = await Task.Run(() => File.Open(imagePath, FileMode.Open));
                return Result.Success((Stream)fileStream);
            }
            catch (Exception ex)
            {
                return Result.Failure<Stream>($"Failed to open image file: {ex.Message}");
            }
        }        
        public async Task<Result<string>> UpdateAsync(IFormFile newImage, string oldImage, ImageType imageType)
        {
            string directory = CombinePathByImageType(imageType);
            string imagePathOld = Path.Combine(directory, oldImage);

            if (!File.Exists(imagePathOld))
                return Result.Failure<string>($"Image for update by path: {imagePathOld} not found!");

            try
            {
                await Task.Run(() => File.Delete(imagePathOld));

                using (FileStream fileStream = new FileStream(path: imagePathOld, FileMode.CreateNew))
                    await newImage.CopyToAsync(fileStream);

                return Result.Success($"Image {imagePathOld} has been deleted!");
            }
            catch (Exception ex)
            {
                return Result.Failure<string>($"Failed to delete and update image file: {ex.Message}");
            }
        }

        private string CombinePathByImageType(ImageType imageType) => imageType switch
        {
            ImageType.PreviewImage => "C:\\ImageCatalog\\PreviewImage\\",
            ImageType.FullImage => "C:\\ImageCatalog\\FullImage\\",
            _ => "",
        };
    }
}
