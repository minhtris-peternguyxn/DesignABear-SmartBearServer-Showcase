using SmartBearServer.Infrastructure.Common;
using SmartBearServer.Model;
using SmartBearServer.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartBearServer.Services.Interfaces
{
    /// <summary>
    /// Defines the contract for all device management operations.
    /// Handles device lifecycle, profile assignment, and mode control using the Result Pattern.
    /// </summary>
    public interface IDeviceService
    {
        /// <summary>
        /// Retrieves all devices belonging to a specific user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>A Result containing a list of Device DTOs.</returns>
        Task<Result<List<DeviceDto>>> GetDevicesByUserAsync(Guid userId);

        /// <summary>
        /// Unpairs a device from its current owner.
        /// </summary>
        /// <param name="userId">The ID of the user requesting unpairing.</param>
        /// <param name="deviceId">The ID of the device to unpair.</param>
        /// <returns>A Result indicating success or failure.</returns>
        Task<Result> UnpairDeviceAsync(Guid userId, string deviceId);

        /// <summary>
        /// Assigns an existing child profile to a device.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="request">The assignment request details.</param>
        /// <returns>A Result containing the updated Device.</returns>
        Task<Result<Device>> AssignProfileAsync(Guid userId, AssignProfileRequest request);

        /// <summary>
        /// Updates the current operating mode of a device's assigned profile.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="request">The mode update request.</param>
        /// <returns>A Result containing the updated Device.</returns>
        Task<Result<Device>> SetDeviceModeAsync(Guid userId, SetDeviceModeRequest request);

        /// <summary>
        /// Toggles the hardware protection (anti-theft) lock.
        /// </summary>
        Task<Result> ToggleHardwareProtectionAsync(Guid userId, string deviceId, bool isEnabled);

        /// <summary>
        /// Creates a new child profile and links it to a device.
        /// </summary>
        Task<Result<ChildProfile>> CreateProfileAsync(Guid userId, string deviceId, UpdateProfileRequest request);

        /// <summary>
        /// Updates an existing child profile's personalization details.
        /// </summary>
        Task<Result<ChildProfile>> UpdateProfileAsync(Guid userId, string profileId, UpdateProfileRequest request);

        /// <summary>
        /// Updates the content safety settings for a child profile.
        /// </summary>
        Task<Result<ChildProfile>> UpdateProfileSafetyAsync(Guid userId, UpdateProfileSafetyRequest request);

        /// <summary>
        /// Resolves a device from its physical serial number without ownership checks.
        /// </summary>
        Task<Result<Device>> GetBySerialNumberAsync(string serialNumber);
        
        /// <summary>
        /// Resolves a device from its ID (MAC/UUID) with caching.
        /// </summary>
        Task<Result<Device>> GetByDeviceIdAsync(string deviceId);

        /// <summary>
        /// Resolves a full, "neat" bear profile configuration optimized for AI consumption.
        /// </summary>
        Task<Result<BearProfileConfig>> GetBearProfileConfigAsync(string? serialNumber = null, string? deviceId = null);

        /// <summary>
        /// Proxies a request to the Bridge to check if a bear is currently online.
        /// </summary>
        Task<Result<bool>> IsDeviceOnlineAsync(string mac);
    }
}
