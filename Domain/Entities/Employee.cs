using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{

    public class Employee
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }
        public byte[]? ProfileImg { get; set; }
        [Required, MaxLength(50)]
        public required string FirstName { get; set; }
        [Required, MaxLength(50)]
        public required string LastName { get; set; }
        public int Age { get; set; }
        [Phone]
        public string? PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public string? Address { get; set; }
        public string? JobTitle { get; set; }
        public decimal Salary { get; set; }
        public virtual Payroll? Payroll { get; set; }
        public int? DepartmentId { get; set; }
        public virtual Department? Department { get; set; }
        public bool IsDeleted { get; set; }
    }
    public enum Gender
    {
        Male,
        Female
    }
}
