using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartBearServer.Data;
using SmartBearServer.Model;
using SmartBearServer.Repositories.Interfaces;

namespace SmartBearServer.Repositories.Implementations
{
    /// <summary>
    /// Implementation of IVoucherRepository for database operations.
    /// Handles everything related to voucher persistence.
    /// </summary>
    public class VoucherRepository : IVoucherRepository
    {
        private readonly AppDbContext _context;

        public VoucherRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task<Voucher?> GetActiveByCodeAsync(string code)
        {
            var upperCode = code.Trim().ToUpper();
            return await _context.Vouchers
                .FirstOrDefaultAsync(v => v.Code.ToUpper() == upperCode && v.IsActive);
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(Voucher voucher)
        {
            _context.Vouchers.Update(voucher);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Voucher>> GetAllAsync()
        {
            return await _context.Vouchers.ToListAsync();
        }
    }
}
