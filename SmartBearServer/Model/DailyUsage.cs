using System;

namespace SmartBearServer.Model
{
    public class DailyUsage
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string DeviceId { get; set; }
        public string ProfileId { get; set; }
        public DateTime DateUtc { get; set; }
        public int AiRequestCount { get; set; }
        public int AudioSecondsUsed { get; set; }
    }
}
