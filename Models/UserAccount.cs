using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace csmvc.Models
{
    public class UserAccount
    {
        [Key]
        public int user_id { get; set; }

        [Required]
        [StringLength(150)]
        public string fullname { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string email { get; set; }

        [Required]
        [StringLength(500)]
        public string hashed_password { get; set; }

        [Phone]
        [StringLength(20)]
        public string phone { get; set; }

        [StringLength(300)]
        public string address { get; set; }

        [Required]
        public DateTime created_at { get; set; } = DateTime.Now;

        [Required]
        public AccountStatus status { get; set; }
    }

    public enum AccountStatus
    {
        Active = 1,
        Inactive = 2
    }
}