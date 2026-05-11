using Google;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.EntityFrameworkCore;
using SmartBearServer.Data;
using SmartBearServer.Model;
using SmartBearServer.Services.Interfaces;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace SmartBearServer.Services.Implementations
{
    public class MediaService : IMediaService
    {
        private readonly IConfiguration _config;
        private readonly StorageClient _storage;
        private readonly AppDbContext _db;
        private readonly ILogger<MediaService> _logger;
        private readonly IEnumerable<ITTSService> _ttsServices;

        private static readonly string[] AudioExtensions = { ".mp3", ".wav", ".m4a", ".aac" };
        private static readonly string[] StoryExtensions = { ".txt", ".mp3", ".wav", ".m4a", ".aac" };

        // Simple blocklist for inappropriate content for children aged 3-10
        private static readonly string[] Blocklist = { "BTS", "KPOP", "REMIX", "ROCK", "VINAHOUSE", "NONSTOP" };

        public MediaService(IConfiguration config, AppDbContext db, ILogger<MediaService> logger, IEnumerable<ITTSService> ttsServices)
        {
            _config = config;
            _db = db;
            _logger = logger;
            _ttsServices = ttsServices;

            var credentialPath = _config["GCP:CredentialsPath"]
                ?? throw new ArgumentException("Missing GCP:CredentialsPath");

            var credential = GoogleCredential.FromJson(System.IO.File.ReadAllText(credentialPath));
            _storage = StorageClient.Create(credential);
        }

        public async Task<StoryPlaybackResponse> ResolveMediaAsync(Device device, string query, MediaType type)
        {
            _logger.LogInformation("[MediaService] Resolving {Type} for query: {Query}", type, query);

            if (IsInappropriate(query))
            {
                _logger.LogWarning("[MediaService] Blocklist match for query: {Query}", query);
                return Reject("Nội dung này không phù hợp với bé đâu, chúng mình chọn bài khác nhé!");
            }

            var bucket = GetBucketName(type);
            if (string.IsNullOrEmpty(bucket))
            {
                return Reject("Lỗi cấu hình hệ thống lưu trữ.");
            }

            // 1. Search Database First (The new reliable way)
            string? foundGcsPath = null;
            string? displayName = null;
            Song? song = null;
            Story? story = null;

            if (type == MediaType.Music)
            {
                if (IsRandomRequest(query, type))
                {
                    song = await _db.Songs
                        .OrderBy(s => Guid.NewGuid())
                        .FirstOrDefaultAsync();
                    _logger.LogInformation("[Media] Random Match Found (Music): '{Name}'", song?.Name);
                }
                else
                {
                    song = await _db.Songs
                        .Where(s => s.Name.ToLower().Contains(query.ToLower()))
                        .OrderBy(s => s.Name.Length)
                        .FirstOrDefaultAsync();
                }

                if (song != null)
                {
                    foundGcsPath = song.GcsPath ?? song.AudioUrl.Split('/').Last();
                    displayName = song.Name;
                    _logger.LogInformation("[Media] Match Found (Music): '{Name}'", displayName);
                }
            }
            else if (type == MediaType.Story)
            {
                if (IsRandomRequest(query, type))
                {
                    story = await _db.Stories
                        .OrderBy(s => Guid.NewGuid())
                        .FirstOrDefaultAsync();
                    _logger.LogInformation("[Media] Random Match Found (Story): '{Name}'", story?.Name);
                }
                else
                {
                    story = await _db.Stories
                        .Where(s => s.Name.ToLower().Contains(query.ToLower()))
                        .OrderBy(s => s.Name.Length)
                        .FirstOrDefaultAsync();
                }

                if (story != null)
                {
                    foundGcsPath = story.GcsPath;
                    displayName = story.Name;
                    _logger.LogInformation("[Media] Match Found (Story): '{Name}'", displayName);
                }
            }

            if (foundGcsPath != null)
            {
                var prefix = GetPrefix(type);

                // Build candidate paths: try raw DB path first, then normalized version
                var candidatePaths = new List<string>();

                // Candidate 1: Raw GcsPath from DB (with prefix if missing)
                var rawPath = foundGcsPath;
                if (!string.IsNullOrEmpty(prefix) && !rawPath.StartsWith(prefix))
                    rawPath = $"{prefix}/{rawPath}";
                candidatePaths.Add(rawPath);

                // Candidate 2: Normalize the filename portion (handles Vietnamese diacritics → ASCII)
                var pathParts = foundGcsPath.Split('/');
                var fileNamePart = pathParts.Last();
                var ext = Path.GetExtension(fileNamePart);
                var baseName = Path.GetFileNameWithoutExtension(fileNamePart);
                var normalizedFileName = Normalize(baseName) + ext;
                var normalizedPath = string.IsNullOrEmpty(prefix) ? normalizedFileName : $"{prefix}/{normalizedFileName}";
                if (!candidatePaths.Contains(normalizedPath))
                    candidatePaths.Add(normalizedPath);

                // Candidate 3: If DB stored a full path with subdirectories, normalize only the last segment
                if (pathParts.Length > 1)
                {
                    var dirPart = string.Join("/", pathParts.Take(pathParts.Length - 1));
                    var altPath = $"{dirPart}/{normalizedFileName}";
                    if (!candidatePaths.Contains(altPath))
                        candidatePaths.Add(altPath);
                }

                foreach (var candidate in candidatePaths)
                {
                    _logger.LogDebug("[Media] Trying GCS path: {Path}", candidate);
                    if (await ExistsAsync(bucket, candidate))
                    {
                        _logger.LogInformation("[Media] Found '{Name}' at GCS path: {Path}", displayName, candidate);
                        var url = CreateSignedUrl(bucket, candidate);
                        return Approve(url);
                    }
                }

                _logger.LogWarning("[Media] DB paths not found in GCS (tried {Count} candidates). Falling back to fuzzy search. Candidates: {Paths}",
                    candidatePaths.Count, string.Join(" | ", candidatePaths));
            }

            // 2. Fallback to GCS matching (Original logic)
            var currentPrefix = GetPrefix(type);

            var extensions = type == MediaType.Story ? StoryExtensions : AudioExtensions;
            var normalized = Normalize(query);

            // 2a. Direct match
            var directCandidates = extensions.SelectMany(ext => new[]
            {
                !string.IsNullOrEmpty(currentPrefix) ? $"{currentPrefix}/{normalized}{ext}" : $"{normalized}{ext}",
                $"{normalized}{ext}"
            }).ToList();

            foreach (var objectName in directCandidates)
            {
                if (await ExistsAsync(bucket, objectName))
                {
                    var url = CreateSignedUrl(bucket, objectName);
                    return Approve(url);
                }
            }

            // 2b. Fuzzy match (requires ListObjects permission)
            try
            {
                var allObjects = _storage.ListObjects(bucket, currentPrefix)
                    .Where(o => !string.IsNullOrWhiteSpace(o.Name) &&
                                extensions.Any(ext => o.Name.EndsWith(ext, StringComparison.OrdinalIgnoreCase)))
                    .ToList();

                if (allObjects.Count > 0)
                {
                    var tokens = normalized.Split('-', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                    var best = allObjects
                        .Select(o => new { o.Name, Score = tokens.Count(t => Normalize(o.Name).Contains(t, StringComparison.OrdinalIgnoreCase)) })
                        .OrderByDescending(x => x.Score)
                        .FirstOrDefault();

                    if (best != null && best.Score > 0)
                    {
                        var signedUrl = CreateSignedUrl(bucket, best.Name);
                        return Approve(signedUrl);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning("[MediaService] Skip GCS fuzzy scan (listing forbidden or error): {Msg}", ex.Message);
            }

            return Reject($"Gấu chưa tìm thấy {GetTypeName(type)} này, để gấu học thêm nhé!");
        }

        public async Task<bool> UploadMediaAsync(Stream stream, string fileName, MediaType type)
        {
            try
            {
                var bucket = GetBucketName(type);
                if (string.IsNullOrEmpty(bucket)) return false;

                var extension = Path.GetExtension(fileName);
                var baseName = Path.GetFileNameWithoutExtension(fileName);
                var normalizedName = Normalize(baseName) + extension;
                
                var prefix = GetPrefix(type);
                var fullPath = string.IsNullOrEmpty(prefix) ? normalizedName : $"{prefix}/{normalizedName}";

                // 1. Upload to GCS with correct ContentType
                var contentType = GetContentType(fileName);
                await _storage.UploadObjectAsync(bucket, fullPath, contentType, stream);
                _logger.LogInformation("[MediaService] Successfully uploaded {Path} to bucket {Bucket} as {ContentType}", fullPath, bucket, contentType);

                // 2. Save to DB
                var cleanName = baseName;
                if (type == MediaType.Music)
                {
                    if (!await _db.Songs.AnyAsync(s => s.GcsPath == fullPath))
                    {
                        _db.Songs.Add(new Song { Name = cleanName, GcsPath = fullPath, Artist = "Gấu Thông Minh", AudioUrl = "GCS" });
                    }
                }
                else if (type == MediaType.Story)
                {
                    if (!await _db.Stories.AnyAsync(s => s.GcsPath == fullPath))
                    {
                        _db.Stories.Add(new Story { Name = cleanName, GcsPath = fullPath, ContentType = contentType });
                    }
                }

                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[MediaService] Upload failed for {FileName}", fileName);
                return false;
            }
        }

        public async Task<List<Song>> GetAllSongsAsync()
        {
            var songs = await _db.Songs.ToListAsync();
            foreach (var song in songs)
            {
                // If the DB has the "GCS" placeholder, we give the app the GcsPath 
                // so it can be saved in the alarm and resolved at FIRE time.
                if (song.AudioUrl == "GCS" && !string.IsNullOrEmpty(song.GcsPath))
                {
                    song.AudioUrl = song.GcsPath;
                }
            }
            return songs;
        }



        public async Task<string> SpeakAsync(string text, string voiceId, string provider)
        {
            // TỰ ĐỘNG SỬA LỖI NẾU PROVIDER BỊ GỬI SAI TỪ FRONTEND
            var actualProvider = provider;
            if (voiceId.Contains("-Neural2-") || voiceId.Contains("-Wavenet-")) 
                actualProvider = "GCP";
            else if (voiceId.Length > 10 && !voiceId.Contains("-")) 
                actualProvider = "ElevenLabs";

            var tts = _ttsServices.FirstOrDefault(s => s.Provider.Equals(actualProvider, StringComparison.OrdinalIgnoreCase))
                      ?? _ttsServices.First();

            var audioData = await tts.GenerateAudio(text, voiceId);

            var bucket = GetBucketName(MediaType.Music); // Reuse music bucket for temp speech files
            var prefix = "temp_speak";
            
            var fileName = $"{provider}_{voiceId}_{Guid.NewGuid()}.mp3";
            var fullPath = $"{prefix}/{fileName}";

            using var ms = new MemoryStream(audioData);
            await _storage.UploadObjectAsync(bucket, fullPath, "audio/mpeg", ms);

            return CreateSignedUrl(bucket!, fullPath);
        }



        public Task<string> GetUploadUrlAsync(string fileName, MediaType type)
        {
            var bucket = GetBucketName(type);
            var prefix = GetPrefix(type);
            
            // Normalize filename to avoid spaces/diacritics issues in GCS
            var extension = Path.GetExtension(fileName);
            var baseName = Path.GetFileNameWithoutExtension(fileName);
            var normalizedName = Normalize(baseName) + extension;
            
            var fullPath = string.IsNullOrEmpty(prefix) ? normalizedName : $"{prefix}/{normalizedName}";

            var credentialPath = _config["GCP:CredentialsPath"] 
                ?? throw new ArgumentException("Missing GCP:CredentialsPath");
            
            var credential = GoogleCredential.FromJson(System.IO.File.ReadAllText(credentialPath));
            var signer = UrlSigner.FromCredential(credential);
            var url = signer.Sign(bucket, fullPath, TimeSpan.FromMinutes(15), HttpMethod.Put);

            return Task.FromResult(url);
        }

        public async Task<bool> ConfirmUploadAsync(string fileName, MediaType type, string? name = null, string? displayInfo = null, string? id = null)
        {
            var extension = Path.GetExtension(fileName);
            var baseName = Path.GetFileNameWithoutExtension(fileName);
            var normalizedName = Normalize(baseName) + extension;
            
            var prefix = GetPrefix(type);
            var fullPath = string.IsNullOrEmpty(prefix) ? normalizedName : $"{prefix}/{normalizedName}";
            
            var cleanName = string.IsNullOrEmpty(name) ? baseName : name;
            var contentType = GetContentType(fileName);

            if (type == MediaType.Music)
            {
                Song? existing = null;
                if (!string.IsNullOrEmpty(id))
                {
                    existing = await _db.Songs.FindAsync(id);
                }
                
                if (existing == null)
                {
                    existing = await _db.Songs.FirstOrDefaultAsync(s => s.GcsPath == fullPath);
                }

                if (existing == null)
                {
                    var all = await _db.Songs.ToListAsync();
                    existing = all.FirstOrDefault(s => Normalize(s.Name) == Normalize(cleanName));
                }

                if (existing != null)
                {
                    existing.Name = cleanName;
                    existing.GcsPath = fullPath;
                    existing.Artist = displayInfo ?? "Gấu Thông Minh";
                    existing.AudioUrl = "GCS";
                    _db.Songs.Update(existing);
                }
                else
                {
                    _db.Songs.Add(new Song { 
                        Name = cleanName, 
                        GcsPath = fullPath, 
                        Artist = displayInfo ?? "Gấu Thông Minh", 
                        AudioUrl = "GCS" 
                    });
                }
            }
            else if (type == MediaType.Story)
            {
                Story? existing = null;
                if (!string.IsNullOrEmpty(id))
                {
                    existing = await _db.Stories.FindAsync(id);
                }

                if (existing == null)
                {
                    existing = await _db.Stories.FirstOrDefaultAsync(s => s.GcsPath == fullPath);
                }

                if (existing == null)
                {
                    var all = await _db.Stories.ToListAsync();
                    existing = all.FirstOrDefault(s => Normalize(s.Name) == Normalize(cleanName));
                }

                if (existing != null)
                {
                    existing.Name = cleanName;
                    existing.GcsPath = fullPath;
                    existing.ContentType = contentType;
                    _db.Stories.Update(existing);
                }
                else
                {
                    _db.Stories.Add(new Story { 
                        Name = cleanName, 
                        GcsPath = fullPath, 
                        ContentType = contentType
                    });
                }
            }

            await _db.SaveChangesAsync();
            return true;
        }

        private string GetContentType(string fileName)
        {
            var ext = Path.GetExtension(fileName).ToLowerInvariant();
            return ext switch
            {
                ".txt" => "text/plain",
                ".mp3" => "audio/mpeg",
                ".wav" => "audio/wav",
                ".m4a" => "audio/mp4",
                ".aac" => "audio/aac",
                _ => "application/octet-stream"
            };
        }



        private string? GetBucketName(MediaType type) => type switch
        {
            MediaType.Story => _config["GCS:StoryBucket"],
            MediaType.Music => _config["GCS:MusicBucket"],
            MediaType.DemoVoice => _config["GCS:DemoVoiceBucket"],
            _ => null
        };

        private string GetPrefix(MediaType type) => type switch
        {
            MediaType.Story => _config["GCS:StoryPrefix"] ?? "stories",
            MediaType.Music => _config["GCS:MusicPrefix"] ?? "music",
            MediaType.DemoVoice => _config["GCS:DemoVoicePrefix"] ?? "voices",
            _ => ""
        };

        private string GetTypeName(MediaType type) => type switch
        {
            MediaType.Story => "truyện",
            MediaType.Music => "bài hát",
            MediaType.DemoVoice => "giọng mẫu",
            _ => "nội dung"
        };

        private bool IsInappropriate(string query)
        {
            var upper = query.ToUpperInvariant();
            return Blocklist.Any(b => upper.Contains(b));
        }

        private async Task<bool> ExistsAsync(string bucket, string objectName)
        {
            try
            {
                await _storage.GetObjectAsync(bucket, objectName);
                return true;
            }
            catch (GoogleApiException ex) when (ex.HttpStatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return false;
            }
            catch
            {
                return false;
            }
        }

        private string CreateSignedUrl(string bucket, string objectName)
        {
            var credentialPath = _config["GCP:CredentialsPath"]
                ?? throw new ArgumentException("Missing GCP:CredentialsPath");

            var credential = GoogleCredential.FromJson(System.IO.File.ReadAllText(credentialPath));
            var signer = UrlSigner.FromCredential(credential);
            var minutes = int.TryParse(_config["GCS:SignedUrlMinutes"], out var m) ? m : 60;

            return signer.Sign(bucket, objectName, TimeSpan.FromMinutes(minutes), HttpMethod.Get);
        }

        private static StoryPlaybackResponse Reject(string reason)
        {
            return new StoryPlaybackResponse { Code = 200, Status = "reject", Message = reason, StreamingURL = null };
        }

        private static StoryPlaybackResponse Approve(string url)
        {
            return new StoryPlaybackResponse { Code = 200, Status = "approved", Message = "Đã tìm thấy nội dung cho bé rồi đây!", StreamingURL = url };
        }

        public static string Normalize(string text)
        {
            var lower = text.Trim().ToLowerInvariant();
            var formD = lower.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();
            foreach (var c in formD)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }
            var noDiacritics = sb.ToString().Normalize(NormalizationForm.FormC).Replace('đ', 'd');
            noDiacritics = Regex.Replace(noDiacritics, @"[^a-z0-9\s\-_/.]", " ");
            noDiacritics = Regex.Replace(noDiacritics, @"\s+", "-");
            return noDiacritics.Trim('-');
        }

        private bool IsRandomRequest(string query, MediaType type)
        {
            var q = query.ToLower().Trim();
            // Expanded keywords for Vietnamese and common AI-generated English tags
            string[] randomKeywords = { 
                "random", "any", "anything", "whatever",
                "bất kỳ", "ngẫu nhiên", "nào đó", "đi", "một bài", "một truyện", 
                "con gấu", "gì đó", "gì cũng được", "bài gì", "bài nào", "truyện nào", "truyện gì"
            };

            // Log for debugging
            _logger.LogDebug("[MediaService] Checking IsRandomRequest for query: '{Query}'", query);

            // Nếu query quá ngắn (ví dụ: chỉ nói "Kể chuyện")
            if (string.IsNullOrEmpty(q) || q.Length < 2) return true;

            // Nếu chứa từ khóa ngẫu nhiên
            if (randomKeywords.Any(k => q.Contains(k))) return true;

            // Nếu query trùng với tên loại hình
            if (type == MediaType.Music && (q == "nhạc" || q == "bài hát" || q == "hát" || q == "ca nhạc")) return true;
            if (type == MediaType.Story && (q == "truyện" || q == "kể chuyện" || q == "đọc truyện" || q == "câu chuyện")) return true;

            return false;
        }
    }
}
