using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace csmvc.Models
{
    public class Voucher
    {
        [Key]
        public int voucher_id { get; set; }

        [Required]
        [StringLength(50)]
        public string code { get; set; }

        [StringLength(500)]
        public string description { get; set; }

        // Giảm theo %
        [Range(0, 100)]
        public int? discount_percent { get; set; }

        // Giảm theo số tiền
        [Column(TypeName = "decimal(18,2)")]
        public decimal? discount_amount { get; set; }

        [Required]
        public DateTime start_date { get; set; }

        [Required]
        public DateTime end_date { get; set; }

        [Required]
        public int quantity { get; set; }

        [Required]
        public VoucherStatus status { get; set; }
    }

    public enum VoucherStatus
    {
        Active = 1,
        Expired = 2,
        Used = 3
    }
}