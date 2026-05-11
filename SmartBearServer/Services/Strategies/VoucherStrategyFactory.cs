using SmartBearServer.Model;
using System;

namespace SmartBearServer.Services.Strategies
{
    public class VoucherStrategyFactory
    {
        public IVoucherStrategy GetStrategy(Voucher voucher)
        {
            if (voucher.FlatPriceValue.HasValue)
            {
                return new FlatPriceStrategy();
            }
            
            if (voucher.FixedDiscountValue.HasValue)
            {
                return new FixedDiscountStrategy();
            }

            if (voucher.DiscountPercentage.HasValue)
            {
                return new PercentageDiscountStrategy();
            }

            throw new InvalidOperationException("Voucher has no valid discount mechanism.");
        }
    }
}
