using System.Text.Json.Serialization;

namespace SmartBearServer.Services
{
    public class GeminiActionResponse
    {
        [JsonPropertyName("action")]
        public string Action { get; set; }

        [JsonPropertyName("song_name")]
        public string SongName { get; set; }
    }
}
