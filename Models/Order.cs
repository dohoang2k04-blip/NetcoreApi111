using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace csmvc.Models
{
    public class Order
    {
        [Key]
        public int order_id { get; set; }

        // ===== Foreign Keys =====
        [Required]
        public int user_id { get; set; }

        public int? employee_id { get; set; }

        public int? voucher_id { get; set; }

        [ForeignKey("user_id")]
        public virtual UserAccount User { get; set; }

        [ForeignKey("employee_id")]
        public virtual Employee Employee { get; set; }

        [ForeignKey("voucher_id")]
        public virtual Voucher Voucher { get; set; }

        // ===== Order Info =====
        [Required]
        public DateTime order_date { get; set; } = DateTime.Now;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal total_amount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal final_amount { get; set; }

        [Required]
        public PaymentMethod payment_method { get; set; }

        [Required]
        public PaymentStatus payment_status { get; set; }

        [Required]
        public ShippingStatus shipping_status { get; set; }

        [Required]
        [StringLength(500)]
        public string shipping_address { get; set; }

        // Quan hệ 1 - nhiều với OrderDetail (nếu có)
       // public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }

    public enum PaymentMethod
    {
        COD = 1,
        BankTransfer = 2,
        EWallet = 3
    }

    public enum PaymentStatus
    {
        Pending = 1,
        Paid = 2,
        Failed = 3
    }

    public enum ShippingStatus
    {
        Processing = 1,
        Shipping = 2,
        Delivered = 3,
        Cancelled = 4
    }
}