using System.Collections.Generic;
using System.Threading.Tasks;
using SmartBearServer.Model;

namespace SmartBearServer.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<PendingOrder>> GetAllOrdersAsync();
        Task<long> GetFulfilledRevenueAsync();
        Task<PendingOrder?> GetByCodeAsync(long orderCode);
        Task AddAsync(PendingOrder order);
        Task UpdateAsync(PendingOrder order);
    }
}
