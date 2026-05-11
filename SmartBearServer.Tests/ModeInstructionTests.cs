using SmartBearServer.Model;
using SmartBearServer.Services.Strategies;
using Xunit;

namespace SmartBearServer.Tests
{
    public class ModeInstructionTests
    {
        private readonly ModeInstructionStrategyFactory _factory;

        public ModeInstructionTests()
        {
            _factory = new ModeInstructionStrategyFactory();
        }

        [Fact]
        public void MathStrategy_ShouldReturnBabyInstruction_WhenCategoryIsBaby()
        {
            // Arrange
            var strategy = new MathInstructionStrategy();
            
            // Act
            var instruction = strategy.GetInstruction(BearCategory.Baby);

            // Assert
            Assert.Contains("Baby Math", instruction);
            Assert.Contains("1 to 10", instruction);
        }

        [Fact]
        public void MathStrategy_ShouldReturnJuniorInstruction_WhenCategoryIsJunior()
        {
            // Arrange
            var strategy = new MathInstructionStrategy();
            
            // Act
            var instruction = strategy.GetInstruction(BearCategory.Junior);

            // Assert
            Assert.Contains("Socratic", instruction);
            Assert.Contains("6–10", instruction);
        }

        [Theory]
        [InlineData("Math", typeof(MathInstructionStrategy))]
        [InlineData("Bilingual", typeof(BilingualInstructionStrategy))]
        [InlineData("Unknown", typeof(NormalConversationStrategy))]
        [InlineData(null, typeof(NormalConversationStrategy))]
        public void Factory_ShouldReturnCorrectStrategy(string mode, System.Type expectedType)
        {
            // Act
            var strategy = _factory.GetStrategy(mode);

            // Assert
            Assert.IsType(expectedType, strategy);
        }
    }
}
