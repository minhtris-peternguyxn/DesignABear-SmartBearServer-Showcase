using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Configuration;
using SmartBearServer.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace SmartBearServer.Services.Implementations
{
    public class GcpStorageService : IStorageService
    {
        private readonly StorageClient? _storage;

        public GcpStorageService(IConfiguration config)
        {
            var credentialPath = config["GCP:CredentialsPath"];
            if (!string.IsNullOrEmpty(credentialPath))
            {
                try 
                {
                    var credential = GoogleCredential.FromJson(System.IO.File.ReadAllText(credentialPath));
                    _storage = StorageClient.Create(credential);
                }
                catch (Exception ex)
                {
                    // Use a proper logger here ideally
                    Console.WriteLine($"[GcpStorageService] GCP Storage initialization failed: {ex.Message}");
                }
            }
        }

        public async Task<double> CalculateBucketStorageAsync(string bucketName)
        {
            if (_storage == null) return 0;
            try
            {
                var objects = _storage.ListObjectsAsync(bucketName);
                long totalBytes = 0;
                await foreach (var obj in objects)
                {
                    totalBytes += (long)(obj.Size ?? 0);
                }
                return totalBytes;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[GcpStorageService] Storage calculation failed for {bucketName}: {ex.Message}");
                return 0;
            }
        }
    }
}
