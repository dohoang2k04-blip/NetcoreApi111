using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace csmvc.Models
{
    public class Category
    {
        [Key]
        public int category_id { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        [StringLength(500)]
        public string description { get; set; }

        // Khóa ngoại tự tham chiếu (parent)
        public int? parent_category_id { get; set; }
/*
        [ForeignKey("parent_category_id")]
        public virtual Category ParentCategory { get; set; }

        // Danh sách category con
        public virtual ICollection<Category> ChildCategories { get; set; }*/
    }
}