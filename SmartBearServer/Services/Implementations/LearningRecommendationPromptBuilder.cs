using SmartBearServer.Model;
using SmartBearServer.Services.Interfaces;
using System.Linq;

namespace SmartBearServer.Services
{
    public class LearningRecommendationPromptBuilder : ILearningRecommendationPromptBuilder
    {
        public string Build(ChildProfile profile)
        {
            var recentInteractions = profile.Interactions.Any()
                ? string.Join("\n", profile.Interactions
                    .OrderByDescending(i => i.Timestamp)
                    .Take(15)
                    .Select(i => $"Bé: {i.Request}\nGấu: {i.Response}"))
                : "Chưa có lịch sử tương tác.";

            return $@"
Bạn là một chuyên gia tâm lý giáo dục trẻ em. 
Hãy phân tích nhật ký trò chuyện dưới đây của bé và đưa ra báo cáo gợi ý học tập cho phụ huynh.

THÔNG TIN BÉ:
- Tên: {profile.Name}
- Tuổi: {profile.Age}

NHẬT KÝ TRÒ CHUYỆN GẦN ĐÂY:
{recentInteractions}

YÊU CẦU:
1. Đánh giá tâm trạng và mối quan tâm hiện tại của bé.
2. Gợi ý 3 hoạt động học tập hoặc trò chơi phù hợp để phát triển kỹ năng cho bé dựa trên nội dung trò chuyện.
3. Đưa ra lời khuyên ngắn gọn cho phụ huynh.

Hãy trả lời bằng tiếng Việt, giọng văn chuyên nghiệp, ấm áp và mang tính khuyến khích.
";
        }
    }
}
