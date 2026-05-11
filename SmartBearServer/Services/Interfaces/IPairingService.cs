using SmartBearServer.Infrastructure.Common;
using SmartBearServer.Model;
using SmartBearServer.Model.DTOs;
using System;
using System.Threading.Tasks;

namespace SmartBearServer.Services.Interfaces
{
    /// <summary>
    /// Service for handling device pairing and ownership claim processes.
    /// </summary>
    public interface IPairingService
    {
        /// <summary>
        /// Requests a pairing code for a physical device. 
        /// Ensures the device is registered in the system.
        /// </summary>
        /// <param name="mac">The physical MAC address of the device.</param>
        /// <returns>A Result containing the pairing code, or an error.</returns>
        Task<Result<string>> RequestPairingCodeAsync(string mac);

        /// <param name="requestingUserId">The ID of the user requesting the OTP.</param>
        /// <returns>A Result containing the generated OTP code.</returns>
        Task<Result<string>> RequestOtpAsync(string macOrSerial, Guid requestingUserId);

        /// <summary>
        /// Claims ownership of a device using a valid pairing code.
        /// </summary>
        /// <param name="userId">The ID of the user claiming the device.</param>
        /// <param name="code">The 6-digit pairing code.</param>
        /// <param name="nickname">An optional nickname for the device.</param>
        /// <param name="childName">The name of the child who will use the device.</param>
        /// <returns>A Result containing the linked Device on success.</returns>
        Task<Result<Device>> ClaimDeviceAsync(Guid userId, string code, string? nickname, string? childName = null);

        /// <summary>
        /// Registers a pairing code generated externally (e.g. by the Bridge) into the backend cache.
        /// </summary>
        Task RegisterExternalOtpAsync(string mac, string code);
    }
}
