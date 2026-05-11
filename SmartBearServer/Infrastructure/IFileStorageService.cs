using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace SmartBearServer.Infrastructure
{
    public interface IFileStorageService
    {
        /// <summary>
        /// Saves audio bytes to the local storage and returns the relative path.
        /// </summary>
        Task<string> SaveAudioResponseAsync(byte[] content, string prefix = "ai");

        /// <summary>
        /// Generates a full public URL for a given relative path.
        /// </summary>
        string GetPublicUrl(string relativePath, HttpRequest request);
    }
}
