using Moq;
using SmartBearServer.Model;
using SmartBearServer.Repositories.Interfaces;
using SmartBearServer.Services.Interfaces;
using SmartBearServer.Services.Implementations;
using Xunit;

namespace SmartBearServer.Tests
{
    public class BannedWordServiceTests
    {
        private readonly Mock<IBannedWordRepository> _mockRepo;
        private readonly Mock<IDeviceRepository> _mockDeviceRepo;
        private readonly Mock<ICacheService> _mockCache;
        private readonly BannedWordService _service;

        public BannedWordServiceTests()
        {
            _mockRepo = new Mock<IBannedWordRepository>();
            _mockDeviceRepo = new Mock<IDeviceRepository>();
            _mockCache = new Mock<ICacheService>();
            _service = new BannedWordService(_mockRepo.Object, _mockDeviceRepo.Object, _mockCache.Object);
        }

        [Fact]
        public async Task GetGlobalBannedWords_ShouldReturnOnlyGlobalItems()
        {
            // Arrange
            var globals = new List<BannedWord>
            {
                new BannedWord { Id = 1, Word = "global1", UserId = null },
                new BannedWord { Id = 2, Word = "global2", UserId = null }
            };

            _mockRepo.Setup(r => r.GetGlobalAsync()).ReturnsAsync(globals);

            // Act
            var result = await _service.GetGlobalBannedWordsAsync();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.All(result, r => Assert.Null(r.UserId));
        }

        [Fact]
        public async Task GetForUserOnly_ShouldReturnOnlyPersonalItems()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var personal = new List<BannedWord>
            {
                new BannedWord { Id = 3, Word = "mine", UserId = userId }
            };

            _mockRepo.Setup(r => r.GetForUserOnlyAsync(userId)).ReturnsAsync(personal);

            // Act
            var result = await _service.GetForUserOnlyAsync(userId);

            // Assert
            Assert.Single(result);
            Assert.Equal(userId, result[0].UserId);
        }

        [Fact]
        public async Task AddBannedWord_AsMaster_GlobalFlagTrue_ShouldCreateGlobalRecord()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _mockRepo.Setup(r => r.ExistsInScopeAsync(It.IsAny<string>(), null)).ReturnsAsync(false);

            // Act
            var result = await _service.AddBannedWordAsync(userId, "SystemRule", "admin", true, true);

            // Assert
            Assert.Null(result.UserId);
            Assert.Equal("master", result.CreatedBy);
            _mockRepo.Verify(r => r.AddAsync(It.IsAny<BannedWord>()), Times.Once);
        }

        [Fact]
        public async Task AddBannedWord_AsParent_ShouldAlwaysTieToUserId()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _mockRepo.Setup(r => r.ExistsInScopeAsync(It.IsAny<string>(), userId)).ReturnsAsync(false);

            // Act
            // Even if parent tries to set isGlobal=true, service should force it to userId
            var result = await _service.AddBannedWordAsync(userId, "FamilyRule", "home", true, false);

            // Assert
            Assert.Equal(userId, result.UserId);
            Assert.Equal("parent", result.CreatedBy);
        }

        [Fact]
        public async Task UpdateProfileBannedKeywords_ShouldNormalizeAndSave()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var profileId = "p1";
            var device = new Device 
            { 
                DeviceId = "dev1",
                SerialNumber = "SN1",
                Status = "Active",
                Profile = new ChildProfile { Id = profileId } 
            };
            var keywords = new List<string> { "  Word1  ", "word2", "WORD1" };

            _mockDeviceRepo.Setup(r => r.GetByProfileIdWithProfileAsync(profileId, userId))
                           .ReturnsAsync(device);

            // Act
            await _service.UpdateProfileBannedKeywordsAsync(userId, profileId, keywords);

            // Assert
            Assert.Equal(2, device.Profile.BlockedTopics.Count);
            Assert.Contains("word1", device.Profile.BlockedTopics);
            Assert.Contains("word2", device.Profile.BlockedTopics);
            _mockDeviceRepo.Verify(r => r.UpdateAsync(It.IsAny<Device>()), Times.Once);
        }
    }
}
