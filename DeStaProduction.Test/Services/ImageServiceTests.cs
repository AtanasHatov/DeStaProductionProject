using NUnit.Framework;
using DeStaProduction.Core.Services;
using Moq;
using CloudinaryDotNet;

namespace DeStaProduction.Test.Services
{
    public class ImageServiceTests
    {
        [Test]
        public void Constructor_Should_Create_Instance()
        {
            var mockCloudinary = new Mock<Cloudinary>(new Account("test", "test", "test"));

            var service = new ImageService(mockCloudinary.Object);

            Assert.That(service, Is.Not.Null);
        }
    }
}