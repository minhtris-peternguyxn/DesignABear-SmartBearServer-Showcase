using System;

namespace SmartBearServer.Model
{
    public class Story : ISoftDelete
    {
        public bool IsDeleted { get; set; } = false;
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string GcsPath { get; set; }
        public string ContentType { get; set; } = "text/plain";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
