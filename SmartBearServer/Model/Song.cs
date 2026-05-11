using System;

namespace SmartBearServer.Model
{
    public class Song : ISoftDelete
    {
        public bool IsDeleted { get; set; } = false;
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Artist { get; set; }
        public string AudioUrl { get; set; }
        public string? GcsPath { get; set; }
    }
}
