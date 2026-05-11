using Moq;
using SmartBearServer.Model;
using SmartBearServer.Repositories.Interfaces;
using SmartBearServer.Services.Implementations;
using SmartBearServer.Services.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;

namespace SmartBearServer.Tests
{
    public class RedisChatPersistenceTests
    {
        private readonly Mock<IChatSessionRepository> _mockRepo;
        private readonly Mock<IAIService> _mockAi;
        private readonly Mock<IConnectionMultiplexer> _mockRedis;
        private readonly Mock<IDatabase> _mockDb;
        private readonly Mock<IDistributedCache> _mockCache;
        
        public RedisChatPersistenceTests()
        {
            _mockRepo = new Mock<IChatSessionRepository>();
            _mockAi = new Mock<IAIService>();
            _mockRedis = new Mock<IConnectionMultiplexer>();
            _mockDb = new Mock<IDatabase>();
            _mockCache = new Mock<IDistributedCache>();

            _mockRedis.Setup(r => r.GetDatabase(It.IsAny<int>(), It.IsAny<object>())).Returns(_mockDb.Object);
            _mockRedis.Setup(r => r.IsConnected).Returns(true);
        }

        [Fact]
        public async Task RedisCacheService_EnqueueInteraction_ShouldPushToRedisList()
        {
            // Arrange
            var service = new RedisCacheService(_mockCache.Object, _mockRedis.Object);
            var interaction = new PendingInteractionDto 
            { 
                Request = "Hello", 
                Response = "Hi", 
                SessionId = Guid.NewGuid() 
            };

            // Act
            await service.EnqueueInteractionAsync(interaction);

            // Assert
            _mockDb.Verify(db => db.ListLeftPushAsync(
                It.Is<RedisKey>(k => k.ToString() == "chat:interaction:queue"), 
                It.IsAny<RedisValue>(), 
                It.IsAny<When>(), 
                It.IsAny<CommandFlags>()), 
                Times.Once);
        }

        [Fact]
        public async Task SessionService_SaveInteraction_ShouldUpdateSessionAndAddHistory()
        {
            // Arrange
            var mockScopeFactory = new Mock<IServiceScopeFactory>();
            var service = new SessionService(_mockRepo.Object, _mockAi.Object, mockScopeFactory.Object);
            var sessionId = Guid.NewGuid();
            var interaction = new PendingInteractionDto 
            { 
                DeviceId = "dev1",
                ProfileId = "prof1",
                Request = "Hello", 
                Response = "Hi", 
                SessionId = sessionId,
                Timestamp = DateTime.UtcNow
            };

            var session = new ChatSession { Id = sessionId, ProfileId = "prof1", Interactions = new List<InteractionHistory>() };
            _mockRepo.Setup(r => r.GetByIdAsync(sessionId)).ReturnsAsync(session);

            // Act
            await service.SaveInteractionAsync(interaction);

            // Assert
            Assert.Single(session.Interactions);
            Assert.Equal("Hello", session.Interactions.GetEnumerator().Current?.Request ?? session.Interactions.First().Request);
            _mockRepo.Verify(r => r.UpdateAsync(It.IsAny<ChatSession>()), Times.Once);
            _mockRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task RedisCacheService_DequeueInteractions_ShouldPopFromRedisList()
        {
            // Arrange
            var service = new RedisCacheService(_mockCache.Object, _mockRedis.Object);
            _mockDb.Setup(db => db.ListRightPopAsync(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>()))
                .ReturnsAsync("{\"request\":\"msg1\"}");

            // Act
            var result = await service.DequeueInteractionsAsync(1);

            // Assert
            Assert.Single(result);
            Assert.Contains("msg1", result[0]);
            _mockDb.Verify(db => db.ListRightPopAsync(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>()), Times.Once);
        }
    }
}
