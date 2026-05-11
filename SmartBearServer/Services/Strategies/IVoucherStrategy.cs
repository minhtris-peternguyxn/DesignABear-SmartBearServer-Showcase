using SmartBearServer.Model;

namespace SmartBearServer.Services.Strategies
{
    public interface IVoucherStrategy
    {
        /// <summary>
        /// Calculates the discounted price based on the voucher logic.
        /// </summary>
        int CalculateDiscount(int originalPrice, Voucher voucher);
    }
}
