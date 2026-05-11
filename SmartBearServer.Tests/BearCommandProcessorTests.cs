using Moq;
using SmartBearServer.Model;
using SmartBearServer.Repositories;
using SmartBearServer.Repositories.Interfaces;
using SmartBearServer.Services;
using SmartBearServer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace SmartBearServer.Tests
{
    public class BearCommandProcessorTests
    {
        private readonly Mock<IAIService> _mockAi;
        private readonly Mock<IDeviceRepository> _mockDeviceRepo;
        private readonly Mock<IContentSafetyService> _mockSafety;
        private readonly Mock<IUsageQuotaService> _mockQuota;
        private readonly Mock<ISubscriptionLifecycleService> _mockSubscription;
        private readonly Mock<ISessionService> _mockSession;
        private readonly Mock<ISpeechService> _mockSpeech;
        private readonly Mock<ICacheService> _mockCache;

        private readonly BearCommandProcessor _processor;

        public BearCommandProcessorTests()
        {
            _mockAi = new Mock<IAIService>(); 
            _mockDeviceRepo = new Mock<IDeviceRepository>();
            _mockSafety = new Mock<IContentSafetyService>();
            _mockQuota = new Mock<IUsageQuotaService>();
            _mockSubscription = new Mock<ISubscriptionLifecycleService>(); 
            _mockSession = new Mock<ISessionService>();
            _mockSpeech = new Mock<ISpeechService>();
            _mockCache = new Mock<ICacheService>();

            _processor = new BearCommandProcessor(
                _mockAi.Object,
                _mockDeviceRepo.Object,
                _mockSafety.Object,
                _mockQuota.Object,
                _mockSubscription.Object,
                _mockSession.Object,
                _mockSpeech.Object,
                _mockCache.Object
            );
        }

        [Fact]
        public async Task HandleInteraction_ShouldReturnSafetyMessage_WhenContentIsUnsafe()
        {
            // Arrange
            string profileId = "test-profile-id";
            var device = new Device 
            { 
                DeviceId = "dev1", 
                SerialNumber = "SN1",
                Status = "Active",
                UserId = Guid.NewGuid(),
                Profile = new ChildProfile { Id = profileId } 
            };
            var session = new ChatSession { Id = Guid.NewGuid() };
            string input = "bad word";
            string safetyMessage = "Hãy cẩn thận với lời nói nhé!";

            _mockDeviceRepo.Setup(r => r.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(device);
            _mockSubscription.Setup(s => s.IsAccessible(It.IsAny<ChildProfile>())).Returns(true);
            _mockSession.Setup(s => s.GetOrCreateActiveSessionAsync(It.IsAny<string>())).ReturnsAsync(session);
            
            // Use It.IsAny to escape potential matching or tuple issues
            _mockSafety.Setup(s => s.EvaluateAsync(It.IsAny<string>(), It.IsAny<ChildProfile>(), It.IsAny<Guid?>()))
                .ReturnsAsync((false, safetyMessage, "Inappropriate"));

            _mockAi.Setup(a => a.ProcessToSpeechFallback(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new byte[] { 1, 2, 3 });

            // Act
            var result = await _processor.ProcessTextCommandAsync("dev1", input, "vi-VN");

            // Assert
            Assert.NotEmpty(result);
            Assert.Equal(new byte[] { 1, 2, 3 }, result);
        }

        [Fact]
        public async Task HandleInteraction_ShouldReturnQuotaMessage_WhenAllowedIsFalse()
        {
            // Arrange
            string profileId = "test-profile-id";
            var device = new Device 
            { 
                DeviceId = "dev1", 
                SerialNumber = "SN1",
                Status = "Active",
                UserId = Guid.NewGuid(),
                Profile = new ChildProfile { Id = profileId } 
            };
            var session = new ChatSession { Id = Guid.NewGuid() };
            string input = "Hello";
            string quotaMessage = "Bé ơi, Gấu hết kẹo rồi.";

            _mockDeviceRepo.Setup(r => r.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(device);
            _mockSubscription.Setup(s => s.IsAccessible(It.IsAny<ChildProfile>())).Returns(true);
            _mockSession.Setup(s => s.GetOrCreateActiveSessionAsync(It.IsAny<string>())).ReturnsAsync(session);
            _mockSafety.Setup(s => s.EvaluateAsync(It.IsAny<string>(), It.IsAny<ChildProfile>(), It.IsAny<Guid?>()))
                .ReturnsAsync((true, "", (string?)null));
            
            _mockQuota.Setup(q => q.CanConsumeAiAsync(It.IsAny<Device>()))
                .ReturnsAsync((false, quotaMessage, (string?)null));

            _mockAi.Setup(a => a.ProcessToSpeechFallback(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new byte[] { 4, 5, 6 });

            // Act
            var result = await _processor.ProcessTextCommandAsync("dev1", input, "vi-VN");

            // Assert
            Assert.NotEmpty(result);
            Assert.Equal(new byte[] { 4, 5, 6 }, result);
        }
    }
}
