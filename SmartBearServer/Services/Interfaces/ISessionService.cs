using SmartBearServer.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartBearServer.Services.Interfaces
{
    public interface ISessionService
    {
        Task<ChatSession> GetOrCreateActiveSessionAsync(string profileId);
        Task SaveInteractionAsync(PendingInteractionDto interaction);
    }
}
