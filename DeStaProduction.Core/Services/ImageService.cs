using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeStaProduction.Core.Contracts;
using DeStaProduction.Infrastucture.Claudinary;

namespace DeStaProduction.Core.Services
{
    public class ImageService : IImageService
    {
        private readonly Cloudinary cloudinary;
        public ImageService(Cloudinary cloudinary)
        {
            this.cloudinary = cloudinary;
        }
        public async Task<string> UploadImageAsync(IFormFile file, string fileName)
        {
            if (file == null || file.Length == 0)
                return null;

            using var stream = file.OpenReadStream();

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(fileName, stream),
                Folder = "desta-events"
            };

            var result = await cloudinary.UploadAsync(uploadParams);

            if (result.Error != null)
            {
                throw new Exception($"Cloudinary error: {result.Error.Message}");
            }

            return result.SecureUrl?.ToString();
        }
    }
}
