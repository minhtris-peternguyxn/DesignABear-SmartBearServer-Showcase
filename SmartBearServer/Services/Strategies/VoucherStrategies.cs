using SmartBearServer.Model;
using System;

namespace SmartBearServer.Services.Strategies
{
    public class FlatPriceStrategy : IVoucherStrategy
    {
        public int CalculateDiscount(int originalPrice, Voucher voucher)
        {
            return voucher.FlatPriceValue ?? originalPrice;
        }
    }

    public class FixedDiscountStrategy : IVoucherStrategy
    {
        public int CalculateDiscount(int originalPrice, Voucher voucher)
        {
            return originalPrice - (voucher.FixedDiscountValue ?? 0);
        }
    }

    public class PercentageDiscountStrategy : IVoucherStrategy
    {
        public int CalculateDiscount(int originalPrice, Voucher voucher)
        {
            if (!voucher.DiscountPercentage.HasValue) return originalPrice;
            return originalPrice - (originalPrice * voucher.DiscountPercentage.Value / 100);
        }
    }
}
