using Microsoft.AspNetCore.Mvc;
using SmartBearServer.Infrastructure.Common;
using SmartBearServer.Infrastructure;
using SmartBearServer.Model;
using SmartBearServer.Services;
using SmartBearServer.Repositories;
using System.IO;

using SmartBearServer.Services.Interfaces;
using SmartBearServer.Repositories.Interfaces;
using SmartBearServer.Services.Implementations;

namespace SmartBearServer.Controllers
{
    [ApiController]
    [Route("api/ai")]
    public class AIController : BaseController
    {
        private readonly ISpeechService _speechService;
        private readonly IAIService _aiService;
        private readonly OpenAIService _openAIService;
        private readonly ITTSService _ttsService;
        private readonly IContentSafetyService _safety;
        private readonly IDeviceRepository _deviceRepo;
        private readonly IDeviceService _deviceService;
        private readonly ISessionService _sessionService;
        private readonly IFileStorageService _storageService;
        private readonly ICacheService _cacheService;
        private readonly IUsageQuotaService _quota;

        public AIController(
            ISpeechService speechService, 
            IAIService aiService, 
            OpenAIService openAIService,
            ITTSService ttsService,
            IContentSafetyService safety,
            IDeviceRepository deviceRepo,
            IDeviceService deviceService,
            ISessionService sessionService,
            IFileStorageService storageService,
            ICacheService cacheService,
            IUsageQuotaService quota)
        {
            _speechService = speechService;
            _aiService = aiService;
            _openAIService = openAIService;
            _ttsService = ttsService;
            _safety = safety;
            _deviceRepo = deviceRepo;
            _deviceService = deviceService;
            _sessionService = sessionService;
            _storageService = storageService;
            _cacheService = cacheService;
            _quota = quota;
        }

        [HttpPost("process-voice-openai")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> ProcessVoiceOpenAI(
            [FromQuery] string deviceId,
            [FromQuery] string languageCode = Constants.Languages.Vietnamese)
        {
            try
            {
                using var ms = new MemoryStream();
                await Request.Body.CopyToAsync(ms);
                byte[] audioBytes = ms.ToArray();

                if (audioBytes.Length == 0)
                {
                    return BadRequest("No audio data received.");
                }

                // 1. Fetch Device & Profile context
                string? resolvedDeviceId = GetDeviceId(deviceId);
                var device = await GetDeviceAndProfileAsync(resolvedDeviceId);
                var profile = device?.Profile;

                // Recognize speech
                var sttResult = await _speechService.RecognizeSpeechAsync(audioBytes, languageCode);
                string transcript = sttResult.Transcript;

                if (string.IsNullOrEmpty(transcript))
                {
                    transcript = "hello, gấu ơi";
                }

                // 2. INPUT SAFETY CHECK (Personalized)
                var inputSafety = await _safety.EvaluateAsync(transcript, profile, device?.UserId);
                if (!inputSafety.IsSafe)
                {
                    return await GenerateSafetyResponse(inputSafety.Message, languageCode, device);
                }

                // 2.5. ATOMIC QUOTA CONSUMPTION (Upfront)
                var (quotaAllowed, consumptionType) = await _quota.TryConsumeAiAsync(device!);
                if (!quotaAllowed)
                {
                    return HandleResult(Result<object>.Success(new { response = "Bé ơi, Gấu hết kẹo rồi. Bé bảo ba mẹ mua thêm kẹo cho Gấu nhé!", isBlocked = true }));
                }

                // 3. GPT PROCESSING (OpenAI)
                string aiResponse;
                try
                {
                    aiResponse = await _openAIService.Process(transcript);
                }
                catch (Exception)
                {
                    await _quota.RefundAiAsync(device!, consumptionType!);
                    throw;
                }
                
                // 4. OUTPUT SAFETY CHECK
                var outputSafety = await _safety.EvaluateResponseAsync(aiResponse, profile, device?.UserId);
                if (!outputSafety.IsSafe)
                {
                    await _quota.RefundAiAsync(device!, consumptionType!);
                    return await GenerateSafetyResponse(outputSafety.Message, languageCode, device);
                }

                // Generate audio response
                var audioResponseBytes = await _ttsService.GenerateAudio(aiResponse, Constants.Voices.DefaultVietnamese, languageCode);
                string relativePath = await _storageService.SaveAudioResponseAsync(audioResponseBytes, "openai");
                string audioUrl = _storageService.GetPublicUrl(relativePath, Request);

                // 5. PERSIST INTERACTION (Asynchronous via Redis)
                if (device != null && profile != null)
                {
                    var session = await _sessionService.GetOrCreateActiveSessionAsync(profile.Id);
                    await _cacheService.EnqueueInteractionAsync(new PendingInteractionDto
                    {
                        DeviceId = resolvedDeviceId!,
                        ProfileId = profile.Id,
                        Request = transcript,
                        Response = aiResponse,
                        SessionId = session.Id,
                        Timestamp = DateTime.UtcNow
                    });
                }

                return HandleResult(Result<object>.Success(new
                {
                    transcript = sttResult.Transcript,
                    text = aiResponse,
                    url = audioUrl
                }));
            }
            catch (Exception ex)
            {
                return HandleResult(Result<object>.Failure(new Error("InternalServerError", ex.Message)));
            }
        }

        /// <summary>
        /// Chat endpoint for text-in / text-out interaction.
        /// </summary>
        [HttpPost("chat")]
        public async Task<IActionResult> Chat([FromBody] ChatRequest request)
        {
            if (string.IsNullOrWhiteSpace(request?.Text))
                return HandleResult(Result<object>.Failure(new Infrastructure.Common.Error("BadRequest", "text field is required")));

            try
            {
                // 1. Fetch Device & Profile context
                string? deviceId = GetDeviceId(request.DeviceId);
                var device = await GetDeviceAndProfileAsync(deviceId);
                var profile = device?.Profile;

                // 2. INPUT SAFETY CHECK
                var inputSafety = await _safety.EvaluateAsync(request.Text, profile, device?.UserId);
                if (!inputSafety.IsSafe)
                {
                    return HandleResult(Result<object>.Success(new { response = inputSafety.Message, isBlocked = true }));
                }

                // 2.5. ATOMIC QUOTA CONSUMPTION (Upfront)
                var (quotaAllowed, consumptionType) = await _quota.TryConsumeAiAsync(device!);
                if (!quotaAllowed)
                {
                    return HandleResult(Result<object>.Success(new { response = "Bé ơi, Gấu hết kẹo rồi. Bé bảo ba mẹ mua thêm kẹo cho Gấu nhé!", isBlocked = true }));
                }

                string response;
                try
                {
                    response = await _aiService.Process(request.Text, device);
                }
                catch (Exception)
                {
                    await _quota.RefundAiAsync(device!, consumptionType!);
                    throw;
                }

                // 3. OUTPUT SAFETY CHECK
                var outputSafety = await _safety.EvaluateResponseAsync(response, profile, device?.UserId);
                if (!outputSafety.IsSafe)
                {
                    await _quota.RefundAiAsync(device!, consumptionType!);
                    return HandleResult(Result<object>.Success(new { response = outputSafety.Message, isBlocked = true }));
                }

                // 4. PERSIST INTERACTION (Asynchronous via Redis)
                if (device != null && profile != null)
                {
                    var sess = await _sessionService.GetOrCreateActiveSessionAsync(profile.Id);
                    await _cacheService.EnqueueInteractionAsync(new PendingInteractionDto
                    {
                        DeviceId = deviceId!,
                        ProfileId = profile.Id,
                        Request = request.Text,
                        Response = response,
                        SessionId = sess.Id,
                        Timestamp = DateTime.UtcNow
                    });
                }

                return HandleResult(Result<object>.Success(new { response }));
            }
            catch (Exception ex)
            {
                return HandleResult(Result<object>.Failure(new Error("InternalServerError", ex.Message)));
            }
        }

        private string? GetDeviceId(string? bodyDeviceId)
        {
            // Priority: Header > Query > Body
            string? rawId = null;
            if (Request.Headers.TryGetValue("Device-Header-Id", out var hId)) rawId = hId.ToString();
            else if (Request.Headers.TryGetValue("Device-Id", out var headerId)) rawId = headerId.ToString();
            else if (Request.Query.TryGetValue("deviceId", out var queryId)) rawId = queryId.ToString();
            else rawId = bodyDeviceId;

            if (string.IsNullOrEmpty(rawId)) return null;

            // Normalize for matching: 10:20:BA -> 1020BA
            return rawId.Replace(":", "").Replace("-", "").Replace(" ", "").ToUpperInvariant().Trim();
        }

        /// <summary>
        /// SSE streaming endpoint for chat.
        /// </summary>
        [HttpPost("chat-stream")]
        public async Task ChatStream([FromBody] ChatRequest request)
        {
            if (string.IsNullOrWhiteSpace(request?.Text))
            {
                Response.StatusCode = 400;
                return;
            }

            Response.ContentType = "text/event-stream";
            Response.Headers.Append("Cache-Control", "no-cache");
            Response.Headers.Append("Connection", "keep-alive");

            try
            {
                // 1. Fetch Device & Profile context
                string? deviceId = GetDeviceId(request.DeviceId);
                var device = await GetDeviceAndProfileAsync(deviceId);
                var profile = device?.Profile;

                if (device == null || profile == null)
                {
                    Response.StatusCode = 401;
                    await Response.WriteAsync($"data: [ERROR] Identity verification failed. Request blocked.\n\n");
                    return;
                }
                
                // 2. SESSION MANAGEMENT
                var session = await _sessionService.GetOrCreateActiveSessionAsync(profile.Id);

                // 2. INPUT SAFETY CHECK
                var inputSafety = await _safety.EvaluateAsync(request.Text, profile, device?.UserId);
                if (!inputSafety.IsSafe)
                {
                    await Response.WriteAsync($"data: [BLOCKED] {inputSafety.Message}\n\n");
                    return;
                }

                // 2.5. ATOMIC QUOTA CONSUMPTION (Upfront)
                var (quotaAllowed, consumptionType) = await _quota.TryConsumeAiAsync(device!);
                if (!quotaAllowed)
                {
                    await Response.WriteAsync($"data: [BLOCKED] Bé ơi, Gấu hết kẹo rồi. Bé bảo ba mẹ mua thêm kẹo cho Gấu nhé!\n\n");
                    return;
                }

                var responseBuilder = new System.Text.StringBuilder();
                try
                {
                    await foreach (var chunk in _aiService.StreamProcess(request.Text, device))
                    {
                        // Escape newlines for SSE
                        var safeChunk = chunk.Replace("\n", "\\n").Replace("\r", "");
                        responseBuilder.Append(chunk);
                        await Response.WriteAsync($"data: {safeChunk}\n\n");
                        await Response.Body.FlushAsync();
                    }
                }
                catch (Exception)
                {
                    await _quota.RefundAiAsync(device!, consumptionType!);
                    throw;
                }

                // 3. OUTPUT SAFETY CHECK (Retroactive — content already streamed, but refund candy if unsafe)
                var fullResponse = responseBuilder.ToString();
                var outputSafety = await _safety.EvaluateResponseAsync(fullResponse, profile, device?.UserId);
                if (!outputSafety.IsSafe)
                {
                    await _quota.RefundAiAsync(device!, consumptionType!);
                    Console.WriteLine($"\x1b[31m[AIController] Streamed response failed safety check for profile {profile.Id}. Candy refunded.\x1b[0m");
                }

                // 4. PERSIST INTERACTION (Asynchronous via Redis)
                await _cacheService.EnqueueInteractionAsync(new PendingInteractionDto
                {
                    DeviceId = deviceId!,
                    ProfileId = profile.Id,
                    Request = request.Text,
                    Response = fullResponse,
                    SessionId = session.Id,
                    IsSafe = outputSafety.IsSafe,
                    SafetyCategory = outputSafety.IsSafe ? null : "output_violation",
                    Timestamp = DateTime.UtcNow
                });
                
                // End of stream marker
                await Response.WriteAsync("data: [DONE]\n\n");
                await Response.Body.FlushAsync();
            }
            catch (Exception ex)
            {
                await Response.WriteAsync($"data: [ERROR] {ex.Message.Replace("\n", " ")}\n\n");
                await Response.Body.FlushAsync();
            }
        }
        
        /// <summary>
        /// Returns the current configuration (voice, provider) for a device.
        /// </summary>
        [HttpGet("config")]
        public async Task<IActionResult> GetConfig([FromQuery] string? deviceId = null)
        {
            try
            {
                string? resolvedDeviceId = GetDeviceId(deviceId);
                var device = await GetDeviceAndProfileAsync(resolvedDeviceId);
                var profile = device?.Profile;

                if (profile == null)
                {
                    Console.WriteLine($"[AI] GetConfig: Device {resolvedDeviceId} exists but has no profile. Needs pairing.");
                    return HandleResult(Result<object>.Success(new
                    {
                        needsPairing = true,
                        message = "Thiết bị chưa được ghép đôi hoặc chưa có Profile."
                    }));
                }

                Console.WriteLine($"[AI] GetConfig 200: Device={resolvedDeviceId}, Profile={profile.Id}, Voice={profile.PreferredVoiceId}");
                return HandleResult(Result<object>.Success(new
                {
                    preferredVoiceId = profile.PreferredVoiceId,
                    preferredTtsProvider = profile.PreferredTtsProvider
                }));
            }
            catch (Exception ex)
            {
                return HandleResult(Result<object>.Failure(new Error("InternalServerError", ex.Message)));
            }
        }

        [HttpPost("process-voice")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> ProcessVoice(
            [FromQuery] string deviceId, 
            [FromQuery] string languageCode = Constants.Languages.Vietnamese)
        {
            try
            {
                using var ms = new MemoryStream();
                await Request.Body.CopyToAsync(ms);
                byte[] audioBytes = ms.ToArray();

                if (audioBytes.Length == 0)
                {
                    return BadRequest("No audio data received.");
                }

                // 1. Fetch Device & Profile context
                string? resolvedDeviceId = GetDeviceId(deviceId);
                var device = await GetDeviceAndProfileAsync(resolvedDeviceId);
                var profile = device?.Profile;

                // Recognize speech
                var sttResult = await _speechService.RecognizeSpeechAsync(audioBytes, languageCode);
                string transcript = sttResult.Transcript;

                if (string.IsNullOrEmpty(transcript))
                {
                    transcript = "hello, gấu ơi";
                }

                // 2. INPUT SAFETY CHECK (Personalized)
                var inputSafety = await _safety.EvaluateAsync(transcript, profile, device?.UserId);
                if (!inputSafety.IsSafe)
                {
                    return await GenerateSafetyResponse(inputSafety.Message, languageCode, device);
                }

                // 2.5. ATOMIC QUOTA CONSUMPTION (Upfront)
                var (quotaAllowed, consumptionType) = await _quota.TryConsumeAiAsync(device!);
                if (!quotaAllowed)
                {
                    return await GenerateSafetyResponse("Bé ơi, Gấu hết kẹo rồi. Bé bảo ba mẹ mua thêm kẹo cho Gấu nhé!", languageCode, device);
                }

                // 3. AI PROCESSING (Mode-aware)
                AIService.AISpeechResult result;
                try
                {
                    result = await _aiService.ProcessToSpeech(transcript, device);
                }
                catch (Exception)
                {
                    await _quota.RefundAiAsync(device!, consumptionType!);
                    throw;
                }
                string aiResponse = result.Text;
                var audioResponseBytes = result.Audio;

                // 4. OUTPUT SAFETY CHECK
                var outputSafety = await _safety.EvaluateResponseAsync(aiResponse, profile, device?.UserId);
                if (!outputSafety.IsSafe)
                {
                    await _quota.RefundAiAsync(device!, consumptionType!);
                    return await GenerateSafetyResponse(outputSafety.Message, languageCode, device);
                }
                
                // 5. STORAGE & URL GENERATION
                string relativePath = await _storageService.SaveAudioResponseAsync(audioResponseBytes);
                string audioUrl = _storageService.GetPublicUrl(relativePath, Request);

                // 6. PERSIST INTERACTION (Asynchronous via Redis)
                if (device != null && profile != null)
                {
                    var sess = await _sessionService.GetOrCreateActiveSessionAsync(profile.Id);
                    await _cacheService.EnqueueInteractionAsync(new PendingInteractionDto
                    {
                        DeviceId = resolvedDeviceId!,
                        ProfileId = profile.Id,
                        Request = transcript,
                        Response = aiResponse,
                        SessionId = sess.Id,
                        Timestamp = DateTime.UtcNow
                    });
                }

                return HandleResult(Result<object>.Success(new
                {
                    transcript = sttResult.Transcript,
                    text = aiResponse,
                    url = audioUrl
                }));
            }
            catch (Exception ex)
            {
                return HandleResult(Result<object>.Failure(new Error("InternalServerError", ex.Message)));
            }
        }

        private async Task<Device?> GetDeviceAndProfileAsync(string? deviceId)
        {
            if (string.IsNullOrEmpty(deviceId)) return null;
            var result = await _deviceService.GetBearProfileConfigAsync(deviceId: deviceId);
            return result.IsSuccess ? result.Value.ToDevice() : null;
        }

        private async Task<IActionResult> GenerateSafetyResponse(string safetyMessage, string languageCode, Device? device)
        {
            var audioBytes = await _aiService.ProcessToSpeechFallback(safetyMessage, languageCode);
            string relativePath = await _storageService.SaveAudioResponseAsync(audioBytes, "safety");
            string audioUrl = _storageService.GetPublicUrl(relativePath, Request);

            return HandleResult(Result<object>.Success(new
            {
                transcript = "[Blocked Content]",
                text = safetyMessage,
                url = audioUrl,
                isBlocked = true
            }));
        }
    }

    /// <summary>DTO for POST /api/ai/chat</summary>
    public record ChatRequest(string Text, string? DeviceId = null);
}
