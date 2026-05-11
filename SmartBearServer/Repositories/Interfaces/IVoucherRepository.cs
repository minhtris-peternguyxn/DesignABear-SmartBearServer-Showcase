using System.Threading.Tasks;
using SmartBearServer.Model;

namespace SmartBearServer.Repositories.Interfaces
{
    /// <summary>
    /// Interface for Voucher data access.
    /// Follows the 3-layer architecture.
    /// </summary>
    public interface IVoucherRepository
    {
        /// <summary>
        /// Retrieves an active voucher by its unique code.
        /// </summary>
        /// <param name="code">The voucher code (case-insensitive).</param>
        /// <returns>The matching Voucher or null.</returns>
        Task<Voucher?> GetActiveByCodeAsync(string code);

        /// <summary>
        /// Updates the voucher state (e.g. usage count).
        /// </summary>
        Task UpdateAsync(Voucher voucher);

        /// <summary>
        /// Retrieves all vouchers from the database.
        /// </summary>
        Task<System.Collections.Generic.List<Voucher>> GetAllAsync();
    }
}
