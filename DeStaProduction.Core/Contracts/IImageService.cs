using Microsoft.AspNetCore.Http;

namespace DeStaProduction.Core.Contracts
{
    public interface IImageService
    {
        Task<string> UploadImageAsync(IFormFile file, string fileName);
    }
}