using Google;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using SmartBearServer.Model;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using SmartBearServer.Services.Interfaces;

namespace SmartBearServer.Services.Implementations
{
    public class StoryPlaybackService : IStoryPlaybackService
    {
        private readonly IConfiguration _config;
        private readonly StorageClient _storage;
        private readonly ILogger<StoryPlaybackService> _logger;

        public StoryPlaybackService(IConfiguration config, ILogger<StoryPlaybackService> logger)
        {
            _config = config;
            _logger = logger;

            var credentialPath = _config["GCP:CredentialsPath"]
                ?? throw new ArgumentException("Missing GCP:CredentialsPath");

            var credential = GoogleCredential.FromJson(System.IO.File.ReadAllText(credentialPath));
            _storage = StorageClient.Create(credential);
        }

        public async Task<StoryPlaybackResponse> ResolveStoryAsync(Device device, string inputText)
        {
            _logger.LogInformation("[Story] Incoming request. Text='{Text}'", inputText);

            if (device?.ParentUser?.SubscriptionPlan == null)
            {
                return Reject("Missing parent/subscription plan.", inputText);
            }

            if (!device.ParentUser.SubscriptionPlan.CanTellStoriesOnUserSpeech)
            {
                return Reject("Plan does not allow story feature.", inputText);
            }

            if (string.IsNullOrWhiteSpace(inputText))
            {
                return Reject("Input text is empty.", inputText);
            }

            var bucket = _config["GCS:StoryBucket"];
            if (string.IsNullOrWhiteSpace(bucket))
            {
                return Reject("Missing GCS:StoryBucket config.", inputText);
            }

            var prefix = _config["GCS:StoryPrefix"] ?? "stories";
            var normalized = Normalize(inputText);

            _logger.LogInformation("[Story] Bucket='{Bucket}', Prefix='{Prefix}', Normalized='{Normalized}'",
                bucket, prefix, normalized);

            var directCandidates = new[]
            {
                $"{prefix}/{normalized}.txt",
                $"{normalized}.txt"
            };

            foreach (var objectName in directCandidates)
            {
                if (await ExistsAsync(bucket, objectName))
                {
                    var url = CreateSignedUrl(bucket, objectName);
                    _logger.LogInformation("[Story] Approved by direct match. Object='{ObjectName}'", objectName);
                    return Approve(url);
                }
            }

            var allObjects = _storage.ListObjects(bucket, prefix)
                .Where(o => !string.IsNullOrWhiteSpace(o.Name) &&
                            o.Name.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                .ToList();

            _logger.LogInformation("[Story] Found {Count} text objects with prefix '{Prefix}'", allObjects.Count, prefix);

            // Fallback: nếu prefix không có gì, quét toàn bucket
            if (allObjects.Count == 0)
            {
                allObjects = _storage.ListObjects(bucket, "")
                    .Where(o => !string.IsNullOrWhiteSpace(o.Name) &&
                                o.Name.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                    .ToList();

                _logger.LogInformation("[Story] Fallback root scan found {Count} text objects", allObjects.Count);
            }

            if (allObjects.Count == 0)
            {
                return Reject("No text objects found in bucket.", inputText);
            }

            var tokens = normalized.Split('-', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var best = allObjects
                .Select(o => new
                {
                    o.Name,
                    Score = tokens.Count(t => Normalize(o.Name).Contains(t, StringComparison.OrdinalIgnoreCase))
                })
                .OrderByDescending(x => x.Score)
                .FirstOrDefault();

            var selectedObject = (best != null && best.Score > 0)
                ? best.Name
                : allObjects.First().Name;

            var signedUrl = CreateSignedUrl(bucket, selectedObject);

            _logger.LogInformation(
                "[Story] Approved by fuzzy/fallback. Selected='{SelectedObject}', Score={Score}, TotalObjects={TotalObjects}",
                selectedObject,
                best?.Score ?? 0,
                allObjects.Count);

            return Approve(signedUrl);
        }

        private async Task<bool> ExistsAsync(string bucket, string objectName)
        {
            try
            {
                await _storage.GetObjectAsync(bucket, objectName);
                _logger.LogDebug("[Story] Object exists. Bucket='{Bucket}', Object='{ObjectName}'", bucket, objectName);
                return true;
            }
            catch (GoogleApiException ex) when (ex.HttpStatusCode == System.Net.HttpStatusCode.NotFound)
            {
                _logger.LogDebug("[Story] Object not found. Bucket='{Bucket}', Object='{ObjectName}'", bucket, objectName);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Story] Failed to check object. Bucket='{Bucket}', Object='{ObjectName}'", bucket, objectName);
                return false;
            }
        }

        private string CreateSignedUrl(string bucket, string objectName)
        {
            var credentialPath = _config["GCP:CredentialsPath"]
                ?? throw new ArgumentException("Missing GCP:CredentialsPath");

            var credential = GoogleCredential.FromJson(System.IO.File.ReadAllText(credentialPath));
            var signer = UrlSigner.FromCredential(credential);
            var minutes = int.TryParse(_config["GCS:SignedUrlMinutes"], out var m) ? m : 30;

            var url = signer.Sign(bucket, objectName, TimeSpan.FromMinutes(minutes), HttpMethod.Get);
            _logger.LogInformation("[Story] Signed URL created. Object='{ObjectName}', ExpMinutes={Minutes}", objectName, minutes);
            return url;
        }

        private StoryPlaybackResponse Reject(string reason, string? inputText = null)
        {
            _logger.LogWarning("[Story] Rejected. Reason='{Reason}', Text='{Text}'", reason, inputText);
            return new StoryPlaybackResponse
            {
                Code = 200,
                Status = "reject",
                StreamingURL = null
            };
        }

        private static StoryPlaybackResponse Approve(string url)
        {
            return new StoryPlaybackResponse
            {
                Code = 200,
                Status = "approved",
                StreamingURL = url
            };
        }

        private static string Normalize(string text)
        {
            var lower = text.Trim().ToLowerInvariant();

            var formD = lower.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();
            foreach (var c in formD)
            {
                var uc = CharUnicodeInfo.GetUnicodeCategory(c);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }

            var noDiacritics = sb.ToString().Normalize(NormalizationForm.FormC)
                .Replace('đ', 'd');

            noDiacritics = Regex.Replace(noDiacritics, @"[^a-z0-9\s\-_/.]", " ");
            noDiacritics = Regex.Replace(noDiacritics, @"\s+", "-");
            return noDiacritics.Trim('-');
        }
    }
}