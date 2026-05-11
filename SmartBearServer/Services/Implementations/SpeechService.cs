using Google.Cloud.Speech.V1P1Beta1;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using SmartBearServer.Services.Interfaces;

using SmartBearServer.Services.Interfaces;

namespace SmartBearServer.Services.Implementations
{
    public class SpeechService : ISpeechService
    {
        private readonly SpeechClient _speechClient;

        public SpeechService(IConfiguration configuration)
        {
            var credentialsPath = configuration["GCP:CredentialsPath"];
            if (string.IsNullOrEmpty(credentialsPath))
            {
                throw new Exception("GCP CredentialsPath is not configured in appsettings.json");
            }

            if (!Path.IsPathRooted(credentialsPath))
            {
                credentialsPath = Path.Combine(AppContext.BaseDirectory, credentialsPath);
            }

            var builder = new SpeechClientBuilder
            {
                GoogleCredential = Google.Apis.Auth.OAuth2.GoogleCredential.FromJson(System.IO.File.ReadAllText(credentialsPath))
            };
            _speechClient = builder.Build();
        }

        public async Task<RecognitionResult> RecognizeSpeechAsync(byte[] audioBytes, string languageCode = "vi-VN")
        {
            var resultMetadata = new RecognitionResult
            {
                LanguageCode = languageCode,
                Transcript = string.Empty
            };

            try
            {
                var isMp3 = IsMp3(audioBytes);
                var isM4a = IsM4a(audioBytes);
                var isWav = IsWav(audioBytes);
                
                resultMetadata.Format = isMp3 ? "MP3" : (isM4a ? "M4A" : (isWav ? "WAV" : "RAW_PCM"));

                if (!isMp3 && !isM4a && !isWav)
                {
                    int silenceSamples = 16000 * 2;
                    var padded = new byte[audioBytes.Length + silenceSamples];
                    Buffer.BlockCopy(audioBytes, 0, padded, 0, audioBytes.Length);
                    audioBytes = padded;
                    Console.WriteLine($"[STT] Padded {silenceSamples} bytes of silence.");
                }
                
                var config = new Google.Cloud.Speech.V1P1Beta1.RecognitionConfig
                {
                    LanguageCode = languageCode,
                    Model = "latest_short", 
                    UseEnhanced = true,
                    EnableAutomaticPunctuation = true,
                    ProfanityFilter = true,
                    Encoding = isMp3 
                        ? Google.Cloud.Speech.V1P1Beta1.RecognitionConfig.Types.AudioEncoding.Mp3 
                        : (isM4a ? (Google.Cloud.Speech.V1P1Beta1.RecognitionConfig.Types.AudioEncoding)11 : Google.Cloud.Speech.V1P1Beta1.RecognitionConfig.Types.AudioEncoding.Linear16),
                };

                // Add SPEECH CONTEXT for better accuracy
                var speechContext = new SpeechContext();
                speechContext.Phrases.Add("Gấu ơi");
                speechContext.Phrases.Add("Xin chào");
                speechContext.Phrases.Add("Hát đi");
                speechContext.Phrases.Add("Kể chuyện");
                speechContext.Phrases.Add("Tắt đèn");
                speechContext.Phrases.Add("Bật đèn");
                speechContext.Phrases.Add("Play song");
                speechContext.Phrases.Add("Tell a story");
                config.SpeechContexts.Add(speechContext);

                // BILINGUAL SUPPORT: vi-VN & en-US
                config.AlternativeLanguageCodes.Add(languageCode == "vi-VN" ? "en-US" : "vi-VN");

                if (!isMp3 && !isM4a)
                {
                    if (isWav)
                    {
                        resultMetadata.Channels = GetWavChannels(audioBytes);
                        resultMetadata.SampleRate = GetWavSampleRate(audioBytes);
                    }
                    else
                    {
                        // Default to the requested format for Raw PCM (ESP32-S3)
                        resultMetadata.Channels = 1;      // Mono
                        resultMetadata.SampleRate = 16000; // 16kHz
                    }

                    config.AudioChannelCount = resultMetadata.Channels;
                    config.SampleRateHertz = resultMetadata.SampleRate;
                    
                    if (config.AudioChannelCount > 1) 
                        config.EnableSeparateRecognitionPerChannel = true;
                }
                else
                {
                    resultMetadata.Channels = 0;
                    resultMetadata.SampleRate = 0;
                }

                Console.WriteLine($"[STT] Requesting {resultMetadata.Format}: Lang={languageCode}, Model={config.Model}, Enhanced={config.UseEnhanced}");

                var response = await _speechClient.RecognizeAsync(config, RecognitionAudio.FromBytes(audioBytes));

                if (response.Results.Count > 0)
                {
                    resultMetadata.Transcript = string.Join(" ", response.Results
                        .Select(r => r.Alternatives.Count > 0 ? r.Alternatives[0].Transcript : ""));

                    resultMetadata.LanguageCode = response.Results[0].LanguageCode ?? languageCode;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[STT CRITICAL ERROR] {ex.Message}");
            }

            return resultMetadata;
        }

        private int GetWavSampleRate(byte[] audioBytes)
        {
            if (audioBytes.Length < 28) return 0;
            return BitConverter.ToInt32(audioBytes, 24);
        }

        private int GetWavChannels(byte[] audioBytes)
        {
            if (audioBytes.Length < 24) return 1;
            if (audioBytes[0] == 0x52 && audioBytes[1] == 0x49 && audioBytes[2] == 0x46 && audioBytes[3] == 0x46)
            {
                return BitConverter.ToInt16(audioBytes, 22);
            }
            return 1;
        }

        private bool IsM4a(byte[] audioBytes)
        {
            if (audioBytes.Length < 12) return false;
            // M4A/MP4 starts with 00 00 00 (size) and then 'ftyp' (66 74 79 70)
            return audioBytes[4] == 0x66 && audioBytes[5] == 0x74 && audioBytes[6] == 0x79 && audioBytes[7] == 0x70;
        }

        private bool IsMp3(byte[] audioBytes)
        {
            if (audioBytes.Length < 4) return false;
            if (audioBytes[0] == 0x49 && audioBytes[1] == 0x44 && audioBytes[2] == 0x33) return true;
            if (audioBytes[0] == 0xFF && (audioBytes[1] & 0xE0) == 0xE0) return true;
            return false;
        }

        private bool IsWav(byte[] audioBytes)
        {
            if (audioBytes.Length < 4) return false;
            // WAV starts with 'RIFF' (52 49 46 46)
            return audioBytes[0] == 0x52 && audioBytes[1] == 0x49 && audioBytes[2] == 0x46 && audioBytes[3] == 0x46;
        }

        private byte[] WrapInWavHeader(byte[] pcmData, int sampleRate, int channels)
        {
            using var ms = new MemoryStream();
            using var writer = new BinaryWriter(ms);

            // RIFF header
            writer.Write(System.Text.Encoding.ASCII.GetBytes("RIFF"));
            writer.Write(36 + pcmData.Length);
            writer.Write(System.Text.Encoding.ASCII.GetBytes("WAVE"));

            // fmt chunk
            writer.Write(System.Text.Encoding.ASCII.GetBytes("fmt "));
            writer.Write(16); // Chunk size
            writer.Write((short)1); // Audio format (1 = PCM)
            writer.Write((short)channels);
            writer.Write(sampleRate);
            writer.Write(sampleRate * channels * 2); // Byte rate
            writer.Write((short)(channels * 2)); // Block align
            writer.Write((short)16); // Bits per sample

            // data chunk
            writer.Write(System.Text.Encoding.ASCII.GetBytes("data"));
            writer.Write(pcmData.Length);
            writer.Write(pcmData);

            return ms.ToArray();
        }
    }
}
