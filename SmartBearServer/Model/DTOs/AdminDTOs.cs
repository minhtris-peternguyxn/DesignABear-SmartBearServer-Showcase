using System;
using System.Collections.Generic;

namespace SmartBearServer.Model.DTOs
{
    public class UserDto
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string? Provider { get; set; }
        public bool IsPro { get; set; }
        public int SmartCandies { get; set; }
        public int RoleId { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class ConfirmUploadRequest
    {
        public string? Id { get; set; }
        public string FileName { get; set; }
        public string Category { get; set; }
        public string? Name { get; set; }
        public string? DisplayInfo { get; set; }
    }

    public class GenerateDemoRequest
    {
        public string Text { get; set; }
        public string VoiceId { get; set; }
        public string Provider { get; set; }
    }

    public class RequestUploadRequest
    {
        public string FileName { get; set; }
        public string Category { get; set; }
    }

    public class ChildProfileDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string? Gender { get; set; }
        public string? CurrentMode { get; set; }
        public DateTime? SubscriptionEndUtc { get; set; }
        public SubscriptionStatus SubscriptionStatus { get; set; }
        public int DailyCandyBalance { get; set; }
    }

    public class SongDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Artist { get; set; }
        public string AudioUrl { get; set; }
        public string? GcsPath { get; set; }
    }

    public class StoryDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
        public string GcsPath { get; set; }
        public string? AudioUrl { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
