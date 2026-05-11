namespace SmartBearServer.Model
{
    public class IoTResponse
    {
        public string Text { get; set; }
        public string AudioUrl { get; set; }
    }
    public class MusicPlaybackResponse
    {
        // speak | stream_gcs
        public string Action { get; set; } = "speak";
        public string? Text { get; set; }
        public string? Url { get; set; }
        public string? SongId { get; set; }
        public string? SongName { get; set; }
    }

    public class StoryPlaybackResponse
    {
        public int Code { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? Message { get; set; }
        public string? StreamingURL { get; set; }
    }
}
