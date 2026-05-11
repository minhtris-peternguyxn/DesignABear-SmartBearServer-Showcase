namespace SmartBearServer.Model
{
    public class IoTRequest
    {
        public string DeviceId { get; set; }
        public string Text { get; set; }
    }
    public class MusicRequest
    {
        // Dùng cho cả text/plain và application/json
        // - Không chỉ định bài: chỉ cần Text, ví dụ "phát nhạc cho bé"
        // - Có chỉ định bài: truyền SongName
        public string? Text { get; set; }
        public string? SongName { get; set; }
    }

    public class StoryRequest
    {
        public string? Text { get; set; }
    }
}
