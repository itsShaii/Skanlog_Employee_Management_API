using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Skanlog_Employee_Management_API.Model
{
    public class Employee
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive value.")]
        [Precision(18, 2)]
        public decimal Salary { get; set; }

        [Required]
        public string SSSNumber { get; set; }

        [Required]
        public string PagIbigNumber { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
