using SmartBearServer.Model;
using System;
using System.Threading.Tasks;

namespace SmartBearServer.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByIdAsync(Guid id);
        Task<User> GetByRefreshTokenAsync(string refreshToken);
        Task<User> AddAsync(User user);
        Task UpdateAsync(User user);
        Task<List<User>> GetAllAsync();
    }
}
