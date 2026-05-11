using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace SmartBearServer.Services.Interfaces
{
    public interface ITTSService
    {
        string Provider { get; }
        Task<byte[]> GenerateAudio(string text, string voiceId, string languageCode = "vi-VN");
    }

    public class ElevenLabsService : ITTSService
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _config;

        public string Provider => "ElevenLabs";

        public ElevenLabsService(HttpClient http, IConfiguration config)
        {
            _http = http;
            _config = config;
        }

        public async Task<byte[]> GenerateAudio(string text, string voiceId, string languageCode = "vi-VN")
        {
            var apiKey = _config["ElevenLabs:ApiKey"];
            // If voiceId is empty, fallback to config default
            var finalVoiceId = string.IsNullOrEmpty(voiceId) ? _config["ElevenLabs:VoiceId"] : voiceId;

            var url = $"https://api.elevenlabs.io/v1/text-to-speech/{finalVoiceId}";

            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Add("xi-api-key", apiKey);

            var body = new
            {
                text = text,
                model_id = "eleven_turbo_v2_5",
                language_code = languageCode.Split('-')[0],
                output_format = "mp3_22050_64",
                voice_settings = new
                {
                    stability = 0.4,
                    similarity_boost = 0.8,
                    style = 0.35
                }
            };

            request.Content = JsonContent.Create(body);

            var res = await _http.SendAsync(request);

            if (!res.IsSuccessStatusCode)
            {
                var err = await res.Content.ReadAsStringAsync();
                throw new System.Exception($"ElevenLabs Error: {err}");
            }

            return await res.Content.ReadAsByteArrayAsync();
        }
    }
}
