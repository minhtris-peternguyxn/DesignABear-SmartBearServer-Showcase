using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SmartBearServer.Infrastructure
{
    public class FileStorageService : IFileStorageService
    {
        private readonly IWebHostEnvironment _env;

        public FileStorageService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> SaveAudioResponseAsync(byte[] content, string prefix = "ai")
        {
            string fileName = $"{prefix}_{Guid.NewGuid()}.mp3";
            string relativePath = Path.Combine(Constants.Storage.AudioResponsesPath, fileName);
            
            string webRoot = _env.WebRootPath ?? Path.Combine(_env.ContentRootPath, Constants.Storage.WwwRoot);
            string absolutePath = Path.Combine(webRoot, relativePath);

            Directory.CreateDirectory(Path.GetDirectoryName(absolutePath)!);
            await File.WriteAllBytesAsync(absolutePath, content);

            return relativePath;
        }

        public string GetPublicUrl(string relativePath, HttpRequest request)
        {
            string host = request.Host.Value;
            string scheme = request.Scheme;
            // Ensure path separators are forward slashes for the URL
            string normalizedPath = relativePath.Replace("\\", "/");
            return $"{scheme}://{host}/{normalizedPath}";
        }
    }
}
