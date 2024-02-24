using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;

namespace AuctionSite.Application.Services.Image
{
    public interface IImageService
    {
        Task<Result<string>> CreateImageAsync(IFormFile formFile);
        Task<Result<Stream>> ReadImageAsync(string fileName);
        Task<Result<string>> UpdateAsync(IFormFile newImage, string oldImage);
        Task<Result<string>> DeleteAsync(string oldImage);
    }
}