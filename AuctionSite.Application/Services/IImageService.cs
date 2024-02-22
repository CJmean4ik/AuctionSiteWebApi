using AuctionSite.Application.Model;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;

namespace AuctionSite.Application.Services
{
    public interface IImageService
    {
        Task<Result<string>> CreateImageAsync(IFormFile formFile, ImageType imageType);
        Task<Result<Stream>> ReadImageAsync(string fileName, ImageType imageType);
        Task<Result<string>> UpdateAsync(IFormFile newImage,string oldImage, ImageType imageType);
        Task<Result<string>> DeleteAsync(string oldImage, ImageType imageType);
    }
}