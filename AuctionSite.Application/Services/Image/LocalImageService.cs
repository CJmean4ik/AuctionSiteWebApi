using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;

namespace AuctionSite.Application.Services.Image
{
    public class LocalImageService : IImageService
    {
        public async Task<Result<string>> CreateImageAsync(IFormFile formFile)
        {
            try
            {
                var directory = "C:\\ImageCatalog\\PreviewImage\\";
                var name = formFile.FileName;
                var fullPath = directory + name;
                
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                if (!File.Exists(fullPath))
                {
                    int index = new Random(formFile.FileName.GetHashCode()).Next(1, 10000);
                    string imagecontentType = formFile.FileName.Split(".").Last();
                    name = formFile.Name + index + "." + imagecontentType;
                    fullPath = directory + name;
                }
                
                using (FileStream file = new FileStream(path: fullPath, FileMode.CreateNew))
                    await formFile.CopyToAsync(file);

                return Result.Success(name);
            }
            catch (Exception ex)
            {
                return Result.Failure<string>(ex.Message);
            }
        }
        public async Task<Result<string>> DeleteAsync(string oldImage)
        {
            var directory = "C:\\ImageCatalog\\PreviewImage\\";
            string imagePathOld = Path.Combine(directory, oldImage);

            if (!File.Exists(imagePathOld))
                return Result.Failure<string>($"Image for update by path: {imagePathOld} not found!");

            await Task.Run(() => File.Delete(imagePathOld));

            return Result.Success($"Image {imagePathOld} has been deleted!");
        }
        public async Task<Result<Stream>> ReadImageAsync(string fileName)
        {
            var directory = "C:\\ImageCatalog\\PreviewImage\\";
            string imagePath = Path.Combine(directory, fileName);

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
        public async Task<Result<string>> UpdateAsync(IFormFile newImage, string oldImage)
        {
            var directory = "C:\\ImageCatalog\\PreviewImage\\";
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
    }
}
