using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.EntityFrameworkCore;
using SmartBearServer.Data;
using SmartBearServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SmartBearServer.Services
{
    public class GcsSyncService
    {
        private readonly AppDbContext _db;
        private readonly StorageClient _storage;
        private readonly IConfiguration _config;
        private readonly ILogger<GcsSyncService> _logger;

        public GcsSyncService(AppDbContext db, IConfiguration config, ILogger<GcsSyncService> logger)
        {
            _db = db;
            _config = config;
            _logger = logger;
            
            var credentialPath = _config["GCP:CredentialsPath"]
                ?? throw new ArgumentException("Missing GCP:CredentialsPath");

            var credential = GoogleCredential.FromFile(credentialPath);
            _storage = StorageClient.Create(credential);
        }

        public async Task SyncAllAsync()
        {
            _logger.LogInformation("[Sync] Starting full GCS to DB synchronization...");

            await SyncMusicAsync();
            await SyncStoriesAsync();

            _logger.LogInformation("[Sync] Full synchronization completed.");
        }

        private async Task SyncMusicAsync()
        {
            var bucket = _config["GCS:MusicBucket"] ?? "gau-bear-media-kho-1";
            _logger.LogInformation("[Sync] Syncing Music from bucket '{Bucket}'...", bucket);

            try
            {
                var objects = _storage.ListObjects(bucket, "").Where(o => o.Name.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase)).ToList();
                foreach (var obj in objects)
                {
                    var cleanName = CleanMediaName(obj.Name);
                    var exists = await _db.Songs.AnyAsync(s => s.GcsPath == obj.Name);
                    if (!exists)
                    {
                        _db.Songs.Add(new Song
                        {
                            Name = cleanName,
                            Artist = "Gấu Thông Minh",
                            GcsPath = obj.Name,
                            AudioUrl = ""
                        });
                        _logger.LogInformation("[Sync] Added Song: {Name}", cleanName);
                    }
                }
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Sync] Error syncing music");
            }
        }

        private async Task SyncStoriesAsync()
        {
            var bucket = _config["GCS:StoryBucket"] ?? "gau_media_kechuyen";
            _logger.LogInformation("[Sync] Syncing Stories from bucket '{Bucket}'...", bucket);

            try
            {
                var objects = _storage.ListObjects(bucket, "").Where(o => o.Name.EndsWith(".txt", StringComparison.OrdinalIgnoreCase)).ToList();
                foreach (var obj in objects)
                {
                    var cleanName = CleanMediaName(obj.Name);
                    var exists = await _db.Stories.AnyAsync(s => s.GcsPath == obj.Name);
                    if (!exists)
                    {
                        _db.Stories.Add(new Story
                        {
                            Name = cleanName,
                            GcsPath = obj.Name
                        });
                        _logger.LogInformation("[Sync] Added Story: {Name}", cleanName);
                    }
                }
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Sync] Error syncing stories");
            }
        }

        private string CleanMediaName(string fileName)
        {
            // Remove extensions
            var name = Path.GetFileNameWithoutExtension(fileName);
            
            // Remove common junk like "- YouTube"
            name = Regex.Replace(name, @"\s*-\s*YouTube", "", RegexOptions.IgnoreCase);
            
            // Remove other common patterns (e.g. "Nhạc Thiếu Nhi - Hoạt Hình...")
            name = Regex.Replace(name, @"\s*-\s*Nhạc Thiếu Nhi.*", "", RegexOptions.IgnoreCase);
            name = Regex.Replace(name, @",\s*các bạn ơi.*", "", RegexOptions.IgnoreCase);
            name = Regex.Replace(name, @"\s*-\s*Pig Dance.*", "", RegexOptions.IgnoreCase);

            return name.Trim();
        }
    }
}
