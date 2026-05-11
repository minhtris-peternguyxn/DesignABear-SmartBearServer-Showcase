using System.IO;
using System.Threading.Tasks;

namespace SmartBearServer.Services.Interfaces
{
    public interface IStorageService
    {
        Task<double> CalculateBucketStorageAsync(string bucketName);
    }
}
