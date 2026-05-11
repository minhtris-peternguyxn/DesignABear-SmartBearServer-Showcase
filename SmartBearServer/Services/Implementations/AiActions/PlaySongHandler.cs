using SmartBearServer.Model;
using SmartBearServer.Model.DTOs;
using SmartBearServer.Repositories;
using SmartBearServer.Repositories.Interfaces;
using SmartBearServer.Services.Interfaces;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartBearServer.Services.Implementations.AiActions
{
    public class PlaySongHandler : IAIActionHandler
    {
        public string ActionName => "play_song";
        private readonly MusicService _music;
        private readonly HttpClient _http;

        public PlaySongHandler(MusicService music, HttpClient http)
        {
            _music = music;
            _http = http;
        }

        public async Task<AIActionResult> HandleAsync(string jsonPayload, BearProfileConfig config)
        {
            try
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var actionObj = JsonSerializer.Deserialize<GeminiActionResponse>(jsonPayload, options);
                
                if (actionObj == null || string.IsNullOrEmpty(actionObj.SongName))
                    return new AIActionResult { IsHandled = false };

                Song? song = null;
                var songName = actionObj.SongName.ToLower().Trim();
                string[] randomKeywords = { "random", "bất kỳ", "ngẫu nhiên", "nào đó", "nhạc", "bài hát" };
                
                if (randomKeywords.Contains(songName) || songName.Length < 2)
                {
                    song = await _music.GetRandomSongAsync();
                }
                else
                {
                    song = await _music.FindSongByNameAsync(actionObj.SongName);
                }
                if (song != null && !string.IsNullOrEmpty(song.AudioUrl))
                {
                    var audioBytes = await _http.GetByteArrayAsync(song.AudioUrl);
                    return new AIActionResult
                    {
                        IsHandled = true,
                        ResponseText = $"Sure! I will play {song.Name} for you now.",
                        AudioData = audioBytes,
                        FinalResponse = $"Sure! I will play {song.Name} for you now. [PLAYING_SONG:{song.AudioUrl}]"
                    };
                }
                
                return new AIActionResult 
                { 
                    IsHandled = true, 
                    ResponseText = $"I am sorry, I couldn't find the song {actionObj.SongName}." 
                };
            }
            catch
            {
                return new AIActionResult { IsHandled = false };
            }
        }
    }
}
