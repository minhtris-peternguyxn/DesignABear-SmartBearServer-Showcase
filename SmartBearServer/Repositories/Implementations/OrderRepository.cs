using Microsoft.EntityFrameworkCore;
using SmartBearServer.Data;
using SmartBearServer.Model;
using SmartBearServer.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBearServer.Repositories.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _db;

        public OrderRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<PendingOrder>> GetAllOrdersAsync()
        {
            return await _db.PendingOrders
                .OrderByDescending(o => o.CreatedAtUtc)
                .ToListAsync();
        }

        public async Task<long> GetFulfilledRevenueAsync()
        {
            return await _db.PendingOrders
                .Where(o => o.IsFulfilled)
                .SumAsync(o => (long)o.Amount);
        }

        public async Task<PendingOrder?> GetByCodeAsync(long orderCode)
        {
            return await _db.PendingOrders.FirstOrDefaultAsync(o => o.OrderCode == orderCode);
        }

        public async Task AddAsync(PendingOrder order)
        {
            await _db.PendingOrders.AddAsync(order);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(PendingOrder order)
        {
            _db.PendingOrders.Update(order);
            await _db.SaveChangesAsync();
        }
    }
}
