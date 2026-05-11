using SmartBearServer.Model;
using SmartBearServer.Repositories;
using SmartBearServer.Repositories.Interfaces;
using SmartBearServer.Services.Implementations;
using SmartBearServer.Services.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace SmartBearServer.Services
{
    public class BearCommandProcessor
    {
        private readonly IAIService _ai;
        private readonly IDeviceRepository _deviceRepo;
        private readonly IContentSafetyService _safety;
        private readonly IUsageQuotaService _quota;
        private readonly ISubscriptionLifecycleService _subscriptionLifecycle;
        private readonly ISessionService _session;
        private readonly ISpeechService _speech;
        private readonly ICacheService _cache;

        public BearCommandProcessor(
            IAIService ai,
            IDeviceRepository deviceRepo,
            IContentSafetyService safety,
            IUsageQuotaService quota,
            ISubscriptionLifecycleService subscriptionLifecycle,
            ISessionService session,
            ISpeechService speech,
            ICacheService cache)
        {
            _ai = ai;
            _deviceRepo = deviceRepo;
            _safety = safety;
            _quota = quota;
            _subscriptionLifecycle = subscriptionLifecycle;
            _session = session;
            _speech = speech;
            _cache = cache;
        }

        public async Task<byte[]> ProcessVoiceCommandAsync(string deviceId, Stream audioStream, string languageCode = "en-US")
        {
            var device = await _deviceRepo.GetByIdAsync(deviceId);
            if (device == null || device.Profile == null)
            {
                return await _ai.ProcessToSpeechFallback("Please set up a profile first.", languageCode);
            }

            if (!_subscriptionLifecycle.IsAccessible(device.Profile))
            {
                return await _ai.ProcessToSpeechFallback("Gói đăng ký đã hết hạn. Hãy nhờ bố mẹ gia hạn nhé.", languageCode);
            }

            // Get active session
            var session = await _session.GetOrCreateActiveSessionAsync(device.Profile.Id);

            using var ms = new MemoryStream();
            await audioStream.CopyToAsync(ms);
            var audioBytes = ms.ToArray();

            // Perform STT
            var sttResult = await _speech.RecognizeSpeechAsync(audioBytes, languageCode);
            var text = sttResult.Transcript;
            
            return await HandleInteractionAsync(device, session, text, languageCode);
        }

        public async Task<RecognitionResult> GetTextFromVoiceAsync(byte[] audioBytes, string languageCode = "en-US")
        {
            return await _speech.RecognizeSpeechAsync(audioBytes, languageCode);
        }

        public async Task<byte[]> ProcessTextCommandAsync(string deviceId, string text, string languageCode = "en-US")
        {
            var device = await _deviceRepo.GetByIdAsync(deviceId);
            if (device == null || device.Profile == null)
            {
                return await _ai.ProcessToSpeechFallback("Please set up a profile first.", languageCode);
            }

            if (!_subscriptionLifecycle.IsAccessible(device.Profile))
            {
                return await _ai.ProcessToSpeechFallback("Gói đăng ký đã hết hạn. Hãy nhờ bố mẹ gia hạn nhé.", languageCode);
            }

            var session = await _session.GetOrCreateActiveSessionAsync(device.Profile.Id);
            return await HandleInteractionAsync(device, session, text, languageCode);
        }

        private async Task<byte[]> HandleInteractionAsync(Device device, ChatSession session, string text, string languageCode)
        {
            if (string.IsNullOrEmpty(text))
            {
                var prompt = languageCode.StartsWith("vi") ? "Con có thể nói lại không? Gấu không nghe rõ." : "Could you repeat that? I didn't hear you.";
                return await _ai.ProcessToSpeechFallback(prompt, languageCode);
            }

            // 1. Safety Check
            var (isSafe, message, category) = await _safety.EvaluateAsync(text, device.Profile, device.UserId);
            if (!isSafe)
            {
                // Save unsafe interaction to history via Redis queue for parent review
                await _cache.EnqueueInteractionAsync(new PendingInteractionDto
                {
                    DeviceId = device.DeviceId,
                    ProfileId = device.Profile.Id,
                    Request = text,
                    Response = message,
                    SessionId = session.Id,
                    IsSafe = false,
                    SafetyCategory = category,
                    Timestamp = DateTime.UtcNow
                });
                return await _ai.ProcessToSpeechFallback(message, languageCode);
            }

            // 2. Atomic Quota Consumption (Upfront)
            var (quotaAllowed, consumptionType) = await _quota.TryConsumeAiAsync(device);
            if (!quotaAllowed)
            {
                return await _ai.ProcessToSpeechFallback("Bé ơi, Gấu hết kẹo rồi. Bé bảo ba mẹ mua thêm kẹo cho Gấu nhé!", languageCode);
            }

            // 3. AI Process
            AIService.AISpeechResult aiResult;
            try
            {
                aiResult = await _ai.ProcessToSpeech(text, device);
            }
            catch (Exception)
            {
                await _quota.RefundAiAsync(device, consumptionType!);
                throw;
            }
            
            // Save safe interaction history via Redis queue
            await _cache.EnqueueInteractionAsync(new PendingInteractionDto
            {
                DeviceId = device.DeviceId,
                ProfileId = device.Profile.Id,
                Request = text,
                Response = aiResult.Text,
                SessionId = session.Id,
                IsSafe = true,
                Timestamp = DateTime.UtcNow
            });
            
            var audioSeconds = UsageQuotaService.EstimateAudioSecondsFromMp3Bytes(aiResult.Audio.Length);
            await _quota.ConsumeAudioAsync(device.DeviceId, audioSeconds, device.Profile.Id);

            return aiResult.Audio;
        }
    }
}
