using Microsoft.EntityFrameworkCore;
using SmartBearServer.Data;
using SmartBearServer.Model;
using SmartBearServer.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBearServer.Repositories.Implementations
{
    public class SubscriptionPlanRepository : ISubscriptionPlanRepository
    {
        private readonly AppDbContext _context;

        public SubscriptionPlanRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<SubscriptionPlan>> GetActivePlansAsync()
        {
            return await _context.SubscriptionPlans
                .Where(p => p.IsActive)
                .OrderBy(p => p.Id)
                .ToListAsync();
        }

        public async Task<SubscriptionPlan?> GetByNameAsync(string planName)
        {
            return await _context.SubscriptionPlans
                .FirstOrDefaultAsync(p => p.Name == planName && p.IsActive);
        }

        public async Task<SubscriptionPlan?> GetByIdAsync(int planId)
        {
            return await _context.SubscriptionPlans.FindAsync(planId);
        }
    }
}
