using System;

namespace SmartBearServer.Model
{
    public class IssueDeviceTokenResponse
    {
        public string TokenId { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresAtUtc { get; set; }
    }
}
