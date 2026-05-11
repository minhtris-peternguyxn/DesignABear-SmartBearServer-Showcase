using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartBearServer.Model
{
    /// <summary>
    /// Tracks an in-flight PayOS payment so the webhook knows which user to activate.
    /// Created when the parent initiates checkout; marked fulfilled after successful webhook.
    /// </summary>
    [Table("pending_orders")]
    public class PendingOrder
    {
        /// <summary>PayOS orderCode — used as primary key and for webhook lookup.</summary>
        [Key]
        [Column("order_code")]
        public long OrderCode { get; set; }

        [Required]
        [Column("user_id")]
        public Guid UserId { get; set; }

        /// <summary>Plan name at time of purchase (e.g. "Premium").</summary>
        [MaxLength(50)]
        [Column("plan_name")]
        public string PlanName { get; set; } = "Premium";

        /// <summary>Amount actually charged in VND (after voucher).</summary>
        [Column("amount")]
        public int Amount { get; set; }

        [Column("created_at_utc")]
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

        [Column("is_fulfilled")]
        public bool IsFulfilled { get; set; } = false;

        /// <summary>Type of order: e.g., "Plan" or "Candy".</summary>
        [Column("order_type")]
        public string OrderType { get; set; } = "Plan";

        /// <summary>Number of candies to add if this is a candy order.</summary>
        [Column("candy_quantity")]
        public int CandyQuantity { get; set; } = 0;

        /// <summary>Voucher applied to this order, to be decremented upon fulfillment.</summary>
        [MaxLength(50)]
        [Column("voucher_code")]
        public string? VoucherCode { get; set; }

        /// <summary>The plan type ID (1: Basic, 2: Premium, 3: VIP).</summary>
        [Column("plan_type")]
        public int? PlanType { get; set; }
    }
}
