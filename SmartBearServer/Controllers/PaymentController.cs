using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartBearServer.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Net.payOS.Types;
using SmartBearServer.Data;
using SmartBearServer.Services.Interfaces;
using System.Security.Claims;

namespace SmartBearServer.Controllers
{
    /// <summary>
    /// Manages subscription plans, payment link creation, and order fulfillment via PayOS.
    /// </summary>
    [Route("api/payment")]
    [ApiController]
    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;
        private readonly IPayOSService _payOSService;
        private readonly ISubscriptionLifecycleService _lifecycleService;

        public PaymentController(IPaymentService paymentService, IPayOSService payOSService, ISubscriptionLifecycleService lifecycleService)
        {
            _paymentService = paymentService;
            _payOSService = payOSService;
            _lifecycleService = lifecycleService;
        }

        /// <summary>
        /// Returns the list of active subscription plans.
        /// </summary>
        [HttpGet("plans")]
        public async Task<IActionResult> GetPlans()
        {
            var plans = await _paymentService.GetActivePlansAsync();
            return HandleResult(Result<object>.Success(plans));
        }

        /// <summary>
        /// Returns the subscription status and expiration for the authenticated user.
        /// Includes reconciliation logic for missing webhook events.
        /// </summary>
        [Authorize]
        [HttpGet("status")]
        public async Task<IActionResult> GetStatus()
        {
            var userId = GetUserId();
            if (userId == null) return HandleResult(Result<object>.Failure(new Infrastructure.Common.Error("Unauthorized", "User not found")));

            try
            {
                var status = await _paymentService.GetUserSubscriptionStatusAsync(userId.Value);
                return HandleResult(Result<object>.Success(status));
            }
            catch (Exception ex)
            {
                return HandleResult(Result<object>.Failure(new Infrastructure.Common.Error("NotFound", ex.Message)));
            }
        }

        /// <summary>
        /// Initiates a payment checkout by creating a PayOS payment link.
        /// </summary>
        [Authorize]
        [HttpPost("create-qr")]
        public async Task<IActionResult> CreatePaymentLink([FromBody] PaymentRequest request)
        {
            var userId = GetUserId();
            if (userId == null) return HandleResult(Result<object>.Failure(new Infrastructure.Common.Error("Unauthorized", "User not found")));

            try
            {
                var result = await _paymentService.CreatePaymentLinkAsync(userId.Value, request.VoucherCode, request.PlanType);
                return HandleResult(Result<object>.Success(new
                {
                    result.checkoutUrl,
                    result.qrCode,
                    result.paymentLinkId,
                    orderCode = result.orderCode,
                    amount = result.amount
                }));
            }
            catch (Exception ex)
            {
                return HandleResult(Result<object>.Failure(new Infrastructure.Common.Error("BadRequest", ex.Message)));
            }
        }

        /// <summary>
        /// Initiates a candy purchase by creating a PayOS payment link.
        /// </summary>
        [Authorize]
        [HttpPost("create-candy-qr")]
        public async Task<IActionResult> CreateCandyPaymentLink([FromBody] CandyPaymentRequest request)
        {
            var userId = GetUserId();
            if (userId == null) return HandleResult(Result<object>.Failure(new Infrastructure.Common.Error("Unauthorized", "User not found")));

            try
            {
                var result = await _paymentService.CreateCandyPaymentLinkAsync(userId.Value, request.CandyPackId, request.VoucherCode);
                return HandleResult(Result<object>.Success(new
                {
                    result.checkoutUrl,
                    result.qrCode,
                    result.paymentLinkId,
                    orderCode = result.orderCode,
                    amount = result.amount
                }));
            }
            catch (Exception ex)
            {
                return HandleResult(Result<object>.Failure(new Infrastructure.Common.Error("BadRequest", ex.Message)));
            }
        }

        /// <summary>
        /// Handles the PayOS webhook for payment confirmation.
        /// </summary>
        [HttpPost("payos-webhook")]
        public async Task<IActionResult> PayOSWebhook(WebhookType body)
        {
            try
            {
                WebhookData data = _payOSService.VerifyWebhook(body);
                Console.WriteLine($"[Webhook] Received PayOS confirmation for Order {data.orderCode} (Status: {data.code})");

                if (data.code == "00")
                {
                    await _paymentService.FulfillOrderAsync(data.orderCode);
                }
                return HandleResult(Result.Success());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Webhook] FAILED to process PayOS webhook: {ex.Message}");
                return HandleResult(Result.Failure(new Infrastructure.Common.Error("BadRequest", $"Webhook processing failed: {ex.Message}")));
            }
        }

        /// <summary>
        /// Updates the user's preferred Text-to-Speech provider.
        /// </summary>
        [Authorize]
        [HttpPut("tts-preference")]
        public async Task<IActionResult> UpdateTtsPreference([FromBody] TtsPreferenceRequest request)
        {
            var userId = GetUserId();
            if (userId == null) return HandleResult(Result<object>.Failure(new Infrastructure.Common.Error("Unauthorized", "User not found")));

            var success = await _lifecycleService.UpdateTtsPreferenceAsync(userId.Value, request.Provider);
            if (!success) return HandleResult(Result.Failure(new Infrastructure.Common.Error("Forbidden", "Access Denied")));

            return HandleResult(Result<object>.Success(new { PreferredTtsProvider = request.Provider }));
        }
        
        /// <summary>
        /// Validates a voucher code and calculates the discount for the premium plan.
        /// </summary>
        [Authorize]
        [HttpGet("validate-voucher")]
        public async Task<IActionResult> ValidateVoucher([FromQuery] string code, [FromQuery] int originalAmount)
        {
            try
            {
                var result = await _paymentService.ValidateVoucherAsync(code, originalAmount);
                return HandleResult(Result<object>.Success(result));
            }
            catch (Exception ex)
            {
                return HandleResult(Result<object>.Failure(new Infrastructure.Common.Error("BadRequest", ex.Message)));
            }
        }

        /// <summary>
        /// Development-only endpoint to manually downgrade a user to the base plan.
        /// </summary>
        [Authorize]
        [HttpPost("cancel-pro")]
        public async Task<IActionResult> CancelPro()
        {
            var userId = GetUserId();
            if (userId == null) return HandleResult(Result<object>.Failure(new Infrastructure.Common.Error("Unauthorized", "User not found")));

            var success = await _lifecycleService.CancelProSubscriptionAsync(userId.Value);
            if (!success) return HandleResult(Result.Failure(new Infrastructure.Common.Error("NotFound", "Profile not found")));

            return HandleResult(Result<object>.Success(new { Message = "Successfully downgraded to the Base Plan." }));
        }

        // ──────────────────────────────────────────────────────────────────────

        private Guid? GetUserId()
        {
            var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.TryParse(claim, out var id) ? id : null;
        }
    }

    public class PaymentRequest
    {
        public int PlanType { get; set; } = 2; // Default to Premium
        public string? VoucherCode { get; set; }
    }

    public class CandyPaymentRequest
    {
        public int CandyPackId { get; set; }
        public string? VoucherCode { get; set; }
    }

    public class TtsPreferenceRequest
    {
        public string Provider { get; set; } = "GCP";
    }
}
