using SmartBearServer.Model;
using SmartBearServer.Repositories;
using SmartBearServer.Repositories.Interfaces;

namespace SmartBearServer.Services.Interfaces
{
    public interface IMusicPlaybackService
    {
        Task<MusicPlaybackResponse> ResolveMusicAsync(Device device, MusicRequest request);
    }
    public class MusicPlaybackService : IMusicPlaybackService
    {
        private readonly ISongRepository _songRepo;
        private readonly IConfiguration _config;

        public MusicPlaybackService(ISongRepository songRepo, IConfiguration config)
        {
            _songRepo = songRepo;
            _config = config;
        }

        public async Task<MusicPlaybackResponse> ResolveMusicAsync(Device device, MusicRequest request)
        {
            if (device?.Profile == null)
            {
                return Speak("Gấu chưa được cấu hình hồ sơ. Nhờ bố mẹ kiểm tra hồ sơ bé nhé!");
            }

            if (device.ParentUser?.SubscriptionPlan == null || !device.ParentUser.SubscriptionPlan.CanPlayMusic)
            {
                return Speak("Gói hiện tại chưa hỗ trợ phát nhạc. Nhờ bố mẹ nâng cấp gói Pro giúp bé nhé!");
            }

            // Tuổi ngoài ngưỡng sản phẩm thì từ chối nhẹ nhàng
            if (device.Profile.Age < 3 || device.Profile.Age > 10)
            {
                return Speak("Tính năng này phù hợp cho bé từ 3 đến 10 tuổi nhé.");
            }

            var songs = await _songRepo.GetAllAsync();
            if (songs.Count == 0)
            {
                return Speak("Kho nhạc đang trống, bố mẹ thêm bài hát giúp bé nhé!");
            }

            Song? selectedSong = null;

            // Cách 1: có chỉ định bài cụ thể
            if (!string.IsNullOrWhiteSpace(request.SongName))
            {
                selectedSong = FindBestMatch(songs, request.SongName);
                if (selectedSong == null)
                {
                    return Speak($"Gấu chưa tìm thấy bài {request.SongName}, bé chọn bài khác nhé!");
                }
            }

            // Cách 2: không chỉ định bài cụ thể -> chọn theo text intent
            if (selectedSong == null && !string.IsNullOrWhiteSpace(request.Text))
            {
                selectedSong = FindBestMatch(songs, request.Text);
            }

            // Fallback: chọn ngẫu nhiên
            if (selectedSong == null)
            {
                var random = new Random();
                selectedSong = songs[random.Next(songs.Count)];
            }

            var playableUrl = BuildPlayableUrl(selectedSong.AudioUrl);
            if (string.IsNullOrWhiteSpace(playableUrl))
            {
                return Speak("Đường dẫn nhạc chưa hợp lệ. Bố mẹ kiểm tra lại kho nhạc giúp bé nhé!");
            }

            return new MusicPlaybackResponse
            {
                Action = "stream_gcs",
                Text = $"Gấu phát bài {selectedSong.Name} nhé!",
                Url = playableUrl,
                SongId = selectedSong.Id,
                SongName = selectedSong.Name
            };
        }

        private static Song? FindBestMatch(IEnumerable<Song> songs, string keyword)
        {
            var k = keyword.Trim().ToLowerInvariant();

            // Ưu tiên match tên bài, sau đó match nghệ sĩ
            return songs.FirstOrDefault(s =>
                       !string.IsNullOrWhiteSpace(s.Name) &&
                       s.Name.ToLowerInvariant().Contains(k))
                   ?? songs.FirstOrDefault(s =>
                       !string.IsNullOrWhiteSpace(s.Artist) &&
                       s.Artist.ToLowerInvariant().Contains(k))
                   ?? songs.FirstOrDefault(s =>
                       !string.IsNullOrWhiteSpace(s.Name) &&
                       k.Contains(s.Name.ToLowerInvariant()));
        }

        private string? BuildPlayableUrl(string? audioUrl)
        {
            if (string.IsNullOrWhiteSpace(audioUrl))
            {
                return null;
            }

            var raw = audioUrl.Trim();

            // URL HTTP(S) sẵn có
            if (Uri.TryCreate(raw, UriKind.Absolute, out var absolute) &&
                (absolute.Scheme == Uri.UriSchemeHttp || absolute.Scheme == Uri.UriSchemeHttps))
            {
                return raw;
            }

            // gs://bucket/object -> https://storage.googleapis.com/bucket/object
            if (raw.StartsWith("gs://", StringComparison.OrdinalIgnoreCase))
            {
                var withoutScheme = raw.Substring("gs://".Length);
                var slashIndex = withoutScheme.IndexOf('/');
                if (slashIndex <= 0 || slashIndex == withoutScheme.Length - 1)
                {
                    return null;
                }

                var bucket = withoutScheme[..slashIndex];
                var obj = withoutScheme[(slashIndex + 1)..];
                return $"https://storage.googleapis.com/{bucket}/{obj}";
            }

            // object path thuần: dùng bucket mặc định từ config nếu có
            var defaultBucket = _config["GCS:MusicBucket"];
            if (!string.IsNullOrWhiteSpace(defaultBucket))
            {
                return $"https://storage.googleapis.com/{defaultBucket}/{raw}";
            }

            return null;
        }

        private static MusicPlaybackResponse Speak(string text)
        {
            return new MusicPlaybackResponse
            {
                Action = "speak",
                Text = text
            };
        }
    }
}
