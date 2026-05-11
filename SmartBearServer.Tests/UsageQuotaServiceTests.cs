using Moq;
using SmartBearServer.Data;
using SmartBearServer.Model;
using SmartBearServer.Services.Implementations;
using SmartBearServer.Services.Interfaces;
using System;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace SmartBearServer.Tests
{
    public class UsageQuotaServiceTests
    {
        private readonly Mock<ICacheService> _mockCache;
        private readonly AppDbContext _db;

        public UsageQuotaServiceTests()
        {
            _mockCache = new Mock<ICacheService>();

            // Setup In-Memory DB
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "SmartBearTest_" + Guid.NewGuid().ToString())
                .Options;
            _db = new AppDbContext(options);
        }

        [Fact]
        public async Task CanConsumeAiAsync_ShouldWarmupFromProfile_IfCacheEmpty()
        {
            // Arrange
            var profileId = "prof_123";
            var device = new Device
            {
                DeviceId = "dev_123",
                SerialNumber = "SN123",
                Status = "Active",
                ProfileId = profileId,
                Profile = new ChildProfile
                {
                    Id = profileId,
                    Name = "Test Child",
                    DailyCandyBalance = 10
                }
            };

            _mockCache.Setup(c => c.GetCounterAsync(It.Is<string>(k => k.Contains(profileId))))
                .ReturnsAsync((long?)null);

            var service = new UsageQuotaService(_db, _mockCache.Object);

            // Act
            var result = await service.CanConsumeAiAsync(device);

            // Assert
            Assert.True(result.Allowed);
            Assert.Equal("Daily", result.ConsumptionType);
            
            // Verify warmup happened
            _mockCache.Verify(c => c.SetCounterAsync(
                It.Is<string>(k => k.Contains(profileId)),
                10,
                It.IsAny<TimeSpan?>()), Times.Once);
        }

        [Fact]
        public async Task ConsumeAiAsync_ShouldDecrementCacheAndEnqueueEvent()
        {
            // Arrange
            var profileId = "prof_123";
            var device = new Device
            {
                DeviceId = "dev_123",
                SerialNumber = "SN123",
                Status = "Active",
                ProfileId = profileId,
                Profile = new ChildProfile { Id = profileId }
            };

            _mockCache.Setup(c => c.DecrementAsync(It.Is<string>(k => k.Contains(profileId)), 1))
                .ReturnsAsync(9);

            var service = new UsageQuotaService(_db, _mockCache.Object);

            // Act
            var result = await service.ConsumeAiAsync(device, "Daily");

            // Assert
            Assert.True(result);
            _mockCache.Verify(c => c.DecrementAsync(It.Is<string>(k => k.Contains(profileId)), 1), Times.Once);
            _mockCache.Verify(c => c.EnqueueQuotaUpdateAsync(It.IsAny<object>()), Times.Once);
        }

        [Fact]
        public async Task CanConsumeAudioAsync_ShouldWarmupFromDB_IfCacheEmpty()
        {
            // Arrange
            var deviceId = "dev_123";
            var profileId = "prof_123";
            var device = new Device
            {
                DeviceId = deviceId,
                SerialNumber = "SN123",
                Status = "Active",
                ProfileId = profileId,
                ParentUser = new User 
                { 
                    IsPro = false,
                    Email = "test@example.com",
                    FullName = "Test Parent",
                    PasswordHash = "hashed"
                }
            };

            // Setup DB data
            var usage = new DailyUsage
            {
                DeviceId = deviceId,
                ProfileId = profileId,
                DateUtc = DateTime.UtcNow.Date,
                AudioSecondsUsed = 100
            };
            _db.DailyUsages.Add(usage);
            await _db.SaveChangesAsync();

            _mockCache.Setup(c => c.GetCounterAsync(It.Is<string>(k => k.Contains(deviceId))))
                .ReturnsAsync((long?)null);

            var service = new UsageQuotaService(_db, _mockCache.Object);

            // Act
            var result = await service.CanConsumeAudioAsync(device, 10);

            // Assert
            Assert.True(result.Allowed);
            _mockCache.Verify(c => c.SetCounterAsync(
                It.Is<string>(k => k.Contains(deviceId)),
                100,
                It.IsAny<TimeSpan?>()), Times.Once);
        }

        [Fact]
        public async Task WarmupQuotaAsync_ShouldCallBothAiAndAudioWarmup()
        {
            // Arrange
            var deviceId = "dev_warm";
            var profileId = "prof_warm";
            var device = new Device
            {
                DeviceId = deviceId,
                SerialNumber = "SN_WARM",
                Status = "Active",
                ProfileId = profileId,
                Profile = new ChildProfile { Id = profileId, DailyCandyBalance = 15 },
                ParentUser = new User 
                { 
                    Email = "warm@example.com", 
                    FullName = "Warm", 
                    PasswordHash = "pw" 
                }
            };

            var service = new UsageQuotaService(_db, _mockCache.Object);

            // Act
            await service.WarmupQuotaAsync(device);

            // Assert
            // Verify AI candy warmup
            _mockCache.Verify(c => c.GetCounterAsync(It.Is<string>(k => k.Contains("candy"))), Times.Once);
            // Verify Audio warmup
            _mockCache.Verify(c => c.GetCounterAsync(It.Is<string>(k => k.Contains("audio"))), Times.Once);
        }
    }
}
