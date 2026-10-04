using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace csmvc.Models
{
    public class OrderItem
    {
        [Key]
        public int item_id { get; set; }

        // ===== Foreign Keys =====
        [Required]
        public int order_id { get; set; }

        [Required]
        public int product_id { get; set; }

        [ForeignKey("order_id")]
        public virtual Order Order { get; set; }

        [ForeignKey("product_id")]
        public virtual Product Product { get; set; }

        // ===== Item Info =====
        [Required]
        [Range(1, int.MaxValue)]
        public int quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal unit_price { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal total_price { get; set; }
    }
}