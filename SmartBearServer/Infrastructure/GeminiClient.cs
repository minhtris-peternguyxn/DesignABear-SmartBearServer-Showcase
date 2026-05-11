namespace SmartBearServer.Infrastructure
{
    using System.Text.Json;
    using System.Net.Http.Json;
    using System.Collections.Generic;

    public class GeminiRequest
    {
        public List<GeminiContent> contents { get; set; } = new();
    }

    public class GeminiContent
    {
        public string role { get; set; } = "user";
        public List<GeminiPart> parts { get; set; } = new();
    }

    public class GeminiPart
    {
        public string text { get; set; } = "";
    }

    public class GeminiClient
    {
        private readonly HttpClient _http;
        private readonly string _apiKey;
        private readonly string _modelId;

        public GeminiClient(HttpClient http, IConfiguration config)
        {
            _http = http;
            _apiKey = config["Gemini:ApiKey"] ?? throw new ArgumentException("Gemini:ApiKey missing in appsettings.json");
            _modelId = config["Gemini:ModelId"] ?? "gemini-1.5-flash";
        }

        public virtual async Task<string> Generate(string prompt)
        {

            var body = CreateRequest(prompt);
            var url = $"https://generativelanguage.googleapis.com/v1beta/models/{_modelId}:generateContent?key={_apiKey}";


            var res = await _http.PostAsJsonAsync(url, body);
            var raw = await res.Content.ReadAsStringAsync();

            if (!res.IsSuccessStatusCode)
                throw new Exception($"Gemini Error ({res.StatusCode}): {raw}");

            using var json = JsonDocument.Parse(raw);

            return json.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString();
        }

        public virtual async IAsyncEnumerable<string> StreamGenerate(string prompt, [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var body = CreateRequest(prompt);
            var url = $"https://generativelanguage.googleapis.com/v1beta/models/{_modelId}:streamGenerateContent?alt=sse&key={_apiKey}";

            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = JsonContent.Create(body)
            };
            
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
            using var reader = new StreamReader(stream);

            while (!reader.EndOfStream && !cancellationToken.IsCancellationRequested)
            {
                var line = await reader.ReadLineAsync();
                if (string.IsNullOrWhiteSpace(line)) continue;

                if (line.StartsWith("data: "))
                {
                    var dataStr = line.Substring(6).Trim();
                    if (dataStr == "[DONE]") break;

                    JsonDocument json;
                    try
                    {
                        json = JsonDocument.Parse(dataStr);
                    }
                    catch
                    {
                        continue;
                    }

                    if (json.RootElement.TryGetProperty("candidates", out var candidates) &&
                        candidates.GetArrayLength() > 0 &&
                        candidates[0].TryGetProperty("content", out var content) &&
                        content.TryGetProperty("parts", out var parts) &&
                        parts.GetArrayLength() > 0 &&
                        parts[0].TryGetProperty("text", out var text))
                    {
                        var textVal = text.GetString();
                        if (!string.IsNullOrEmpty(textVal))
                        {
                            yield return textVal;
                        }
                    }
                }
            }
        }

        private GeminiRequest CreateRequest(string prompt)
        {
            return new GeminiRequest
            {
                contents = new List<GeminiContent> {
                    new GeminiContent {
                        role = "user",
                        parts = new List<GeminiPart> {
                            new GeminiPart { text = prompt }
                        }
                    }
                }
            };
        }
    }
}
