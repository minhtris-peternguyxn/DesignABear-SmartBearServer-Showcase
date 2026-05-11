using SmartBearServer.Infrastructure.Common;
using SmartBearServer.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartBearServer.Services.Interfaces
{
    /// <summary>
    /// Service for managing smart bear alarms.
    /// Handles creation, updating, and triggering of alarms using the Result Pattern.
    /// </summary>
    public interface ISmartAlarmService
    {
        /// <summary>
        /// Retrieves all alarms for a specific user.
        /// </summary>
        Task<Result<List<SmartAlarmDto>>> GetMyAlarmsAsync(Guid userId);

        /// <summary>
        /// Retrieves a specific alarm by ID for a user.
        /// </summary>
        Task<Result<SmartAlarmDto>> GetMyAlarmByIdAsync(Guid userId, string alarmId);

        /// <summary>
        /// Creates a new alarm for a device owned by the user.
        /// </summary>
        Task<Result<SmartAlarmDto>> CreateAlarmAsync(Guid userId, SmartAlarmRequest request);

        /// <summary>
        /// Updates an existing alarm's settings.
        /// </summary>
        Task<Result<SmartAlarmDto>> UpdateAlarmAsync(Guid userId, string alarmId, UpdateSmartAlarmRequest request);

        /// <summary>
        /// Toggles an alarm's enabled state.
        /// </summary>
        Task<Result<SmartAlarmDto>> ToggleAlarmAsync(Guid userId, string alarmId);

        /// <summary>
        /// Deletes an alarm.
        /// </summary>
        Task<Result> DeleteAlarmAsync(Guid userId, string alarmId);

        /// <summary>
        /// Background process for polling and firing due alarms.
        /// </summary>
        Task ProcessPendingAlarmsAsync();
    }
}
