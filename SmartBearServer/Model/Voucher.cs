using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartBearServer.Model
{
    [Table("vouchers")]
    public class Voucher
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("code")]
        public string Code { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("discount_percentage")]
        public int? DiscountPercentage { get; set; }

        [Column("fixed_discount_value")]
        public int? FixedDiscountValue { get; set; }

        [Column("flat_price_value")]
        public int? FlatPriceValue { get; set; }

        [Column("max_usage")]
        public int? MaxUsage { get; set; }

        [Column("current_usage")]
        public int CurrentUsage { get; set; } = 0;

        [Column("expiry_date")]
        public DateTime? ExpiryDate { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; } = true;
    }
}
