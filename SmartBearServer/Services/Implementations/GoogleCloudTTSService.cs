using Google.Cloud.TextToSpeech.V1;
using Microsoft.Extensions.Configuration;
using SmartBearServer.Services.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SmartBearServer.Services
{
    public class GoogleCloudTTSService : ITTSService
    {
        private readonly TextToSpeechClient _client;
        public string Provider => "GCP";

        public GoogleCloudTTSService(IConfiguration config)
        {
            var credentialsPath = config["GCP:CredentialsPath"];
            if (string.IsNullOrEmpty(credentialsPath))
            {
                throw new Exception("GCP CredentialsPath is not configured.");
            }

            if (!Path.IsPathRooted(credentialsPath))
            {
                credentialsPath = Path.Combine(AppContext.BaseDirectory, credentialsPath);
            }

            var builder = new TextToSpeechClientBuilder
            {
                CredentialsPath = credentialsPath
            };
            _client = builder.Build();
        }

        public async Task<byte[]> GenerateAudio(string text, string voiceId, string languageCode = "vi-VN")
        {
            // Default GCP voice if none provided
            var finalVoiceId = string.IsNullOrEmpty(voiceId) ? "vi-VN-Neural2-A" : voiceId;

            var input = new SynthesisInput { Ssml = $"<speak>{System.Security.SecurityElement.Escape(text)}<break time=\"200ms\"/></speak>" };

            // Build synthesis request
            var voiceSelection = new VoiceSelectionParams
            {
                LanguageCode = languageCode,
                Name = finalVoiceId
            };

            var audioConfig = new AudioConfig
            {
                AudioEncoding = AudioEncoding.Mp3,
                Pitch = 0.0,
                SpeakingRate = 0.6
            };

            var response = await _client.SynthesizeSpeechAsync(input, voiceSelection, audioConfig);

            using var ms = new MemoryStream();
            response.AudioContent.WriteTo(ms);
            return ms.ToArray();
        }
    }
}
