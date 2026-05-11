using SmartBearServer.Infrastructure;
using SmartBearServer.Model;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using SmartBearServer.Services.Interfaces;
using SmartBearServer.Repositories.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SmartBearServer.Services.Implementations
{
    public class OpenAIService
    {
        private readonly OpenAIClient _openAI;
        private readonly MusicService _music;
        private readonly HttpClient _http;

        public OpenAIService(OpenAIClient openAI, MusicService music, HttpClient http)
        {
            _openAI = openAI;
            _music = music;
            _http = http;
        }

        public async Task<string> Process(string input, Device? device = null)
        {
            var profileContext = "";
            var historyContext = "";

            if (device?.Profile != null)
            {
                var profile = device.Profile;
                profileContext = $@"
Child's Profile:
- Name: {profile.Name}
- Age: {profile.Age}
";
                if (profile.Interactions.Any())
                {
                    var recentInteractions = string.Join("\n", profile.Interactions.OrderByDescending(i => i.Timestamp).Take(3).Select(i => $"Child: {i.Request}\nBear: {i.Response}"));
                    historyContext = $@"
Recent chat history:
{recentInteractions}
";
                }
            }

            var prompt = $@"
You are a friendly smart bear talking to a child (5-10 years old).

CRITICAL LANGUAGE RULE:
- You MUST detect the language of the child's input and respond in EXACTLY that language.
- If the child speaks Vietnamese, you MUST reply ENTIRELY in Vietnamese.
- If the child speaks English, reply in English.

Other Rules:
- Be fun, friendly, and cute.
- Keep it short (1-2 sentences).
- ONLY output the JSON action if the child EXPLICITLY asks to play a song.

JSON action format (ONLY for song requests):
{{ ""action"": ""play_song"", ""song_name"": ""..."" }}

{profileContext}
{historyContext}
Child said: {input}
";

            var response = await _openAI.Generate(prompt);
            var json = ExtractJson(response);

            if (!string.IsNullOrEmpty(json))
            {
                try
                {
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var actionObj = JsonSerializer.Deserialize<OpenAIActionResponse>(json, options);
                    
                    if (actionObj?.Action == "play_song")
                    {
                        var song = await _music.FindSongByNameAsync(actionObj.SongName);
                        if (song != null)
                        {
                            return $"Gấu sẽ mở bài {song.Name} cho bé nghe nhé! [PLAYING_SONG:{song.AudioUrl}]";
                        }
                        else
                        {
                            return $"Gấu xin lỗi, gấu không tìm thấy bài hát {actionObj.SongName} trong kho nhạc.";
                        }
                    }
                }
                catch
                {
                    // Fallback to raw response if JSON parse fails
                }
            }

            return response;
        }

        private string ExtractJson(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return string.Empty;
            var match = Regex.Match(text, @"{.*}", RegexOptions.Singleline);
            return match.Success ? match.Value : string.Empty;
        }
    }
}
