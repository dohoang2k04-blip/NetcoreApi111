using System.ComponentModel.DataAnnotations;

namespace csmvc.Models
{
    public class Employee
    {
        [Key]
        public int employee_id { get; set; }

        [Required]
        [StringLength(150)]
        public string full_name { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string email { get; set; }

        [Required]
        [StringLength(500)]
        public string hashed_password { get; set; }

        [Required]
        public EmployeeRole role { get; set; }

        [Required]
        public EmployeeStatus status { get; set; }
    }

    public enum EmployeeRole
    {
        Admin = 1,
        Staff = 2
    }

    public enum EmployeeStatus
    {
        Active = 1,
        Inactive = 2
    }
}