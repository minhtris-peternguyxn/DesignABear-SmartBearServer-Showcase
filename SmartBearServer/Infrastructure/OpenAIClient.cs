using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using System.Text.Json;

namespace SmartBearServer.Infrastructure
{
    public class OpenAIClient
    {
        private readonly HttpClient _http;
        private readonly string _apiKey;
        private readonly string _baseUrl;
        private readonly string _model;

        public OpenAIClient(HttpClient http, IConfiguration configuration)
        {
            _http = http;
            _apiKey = configuration["OpenAI:ApiKey"];
            _baseUrl = configuration["OpenAI:BaseUrl"]?.TrimEnd('/');
            _model = configuration["OpenAI:Model"] ?? "gpt-3.5-turbo";

            if (string.IsNullOrEmpty(_apiKey))
            {
                throw new Exception("OpenAI ApiKey is not configured in appsettings.json");
            }
        }

        public async Task<string> Generate(string prompt)
        {
            var url = $"{_baseUrl}/chat/completions";

            var body = new
            {
                model = _model,
                messages = new[]
                {
                    new { role = "user", content = prompt }
                },
                temperature = 0.7
            };

            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Add("Authorization", $"Bearer {_apiKey}");
            request.Content = JsonContent.Create(body);

            var res = await _http.SendAsync(request);
            var raw = await res.Content.ReadAsStringAsync();

            Console.WriteLine($"[OpenAI] STATUS: {res.StatusCode}");
            
            if (!res.IsSuccessStatusCode)
            {
                Console.WriteLine($"[OpenAI ERROR] {raw}");
                throw new Exception($"OpenAI Error: {raw}");
            }

            using var json = JsonDocument.Parse(raw);
            return json.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();
        }
    }
}
