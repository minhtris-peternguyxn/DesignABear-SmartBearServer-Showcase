using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Moq;
using SmartBearServer.Infrastructure;
using Xunit;

namespace SmartBearServer.Tests
{
    public class FileStorageTests
    {
        [Fact]
        public void GetPublicUrl_ShouldReturnCorrectUrl_WhenRequestIsAvailable()
        {
            // Arrange
            var mockEnv = new Mock<IWebHostEnvironment>();
            var mockRequest = new Mock<HttpRequest>();

            mockRequest.Setup(r => r.Scheme).Returns("https");
            mockRequest.Setup(r => r.Host).Returns(new HostString("smartbear.vn"));
            
            var service = new FileStorageService(mockEnv.Object);
            string relativePath = "audio/responses/test.mp3";

            // Act
            string absoluteUrl = service.GetPublicUrl(relativePath, mockRequest.Object);

            // Assert
            Assert.Equal("https://smartbear.vn/audio/responses/test.mp3", absoluteUrl);
        }
    }
}
