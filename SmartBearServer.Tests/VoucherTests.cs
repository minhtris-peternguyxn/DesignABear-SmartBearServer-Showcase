using SmartBearServer.Model;
using SmartBearServer.Services.Strategies;
using Xunit;

namespace SmartBearServer.Tests
{
    public class VoucherTests
    {
        private readonly VoucherStrategyFactory _factory;

        public VoucherTests()
        {
            _factory = new VoucherStrategyFactory();
        }

        [Fact]
        public void FixedDiscountStrategy_ShouldReducePriceByFixedAmount()
        {
            // Arrange
            var strategy = new FixedDiscountStrategy();
            var voucher = new Voucher { FixedDiscountValue = 20000 };
            int originalPrice = 100000;

            // Act
            int finalPrice = strategy.CalculateDiscount(originalPrice, voucher);

            // Assert
            Assert.Equal(80000, finalPrice);
        }

        [Fact]
        public void PercentageDiscountStrategy_ShouldReducePriceByPercentage()
        {
            // Arrange
            var strategy = new PercentageDiscountStrategy();
            var voucher = new Voucher { DiscountPercentage = 10 };
            int originalPrice = 100000;

            // Act
            int finalPrice = strategy.CalculateDiscount(originalPrice, voucher);

            // Assert
            Assert.Equal(90000, finalPrice);
        }

        [Fact]
        public void FlatPriceStrategy_ShouldOverridePriceWithFlatValue()
        {
            // Arrange
            var strategy = new FlatPriceStrategy();
            var voucher = new Voucher { FlatPriceValue = 2000 };
            int originalPrice = 100000;

            // Act
            int finalPrice = strategy.CalculateDiscount(originalPrice, voucher);

            // Assert
            Assert.Equal(2000, finalPrice);
        }

        [Theory]
        [InlineData(2000, null, null, typeof(FlatPriceStrategy))]
        [InlineData(null, 5000, null, typeof(FixedDiscountStrategy))]
        [InlineData(null, null, 15, typeof(PercentageDiscountStrategy))]
        public void Factory_ShouldReturnCorrectStrategy(int? flat, int? fixedVal, int? percent, System.Type expectedType)
        {
            // Arrange
            var voucher = new Voucher 
            { 
                FlatPriceValue = flat, 
                FixedDiscountValue = fixedVal, 
                DiscountPercentage = percent 
            };

            // Act
            var strategy = _factory.GetStrategy(voucher);

            // Assert
            Assert.IsType(expectedType, strategy);
        }
    }
}
