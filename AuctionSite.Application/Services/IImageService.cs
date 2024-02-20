using AuctionSite.Application.Model;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuctionSite.Application.Services
{
    public interface IImageService
    {
        Task<Result<T>> GetImage<T>(string fileName, ImageType imageType) where T : class;
        Task<Result<string>> SaveImageAsync(IFormFile formFile, ImageType imageType);
        Result<string> Delete(string fileName, ImageType imageType);
    }
}