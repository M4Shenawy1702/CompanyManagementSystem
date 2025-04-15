using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.EmployeeDto
{
    public class EmployeeDto
    {
        public byte[]? ProfileImg { get; set; }
        [Required, MaxLength(50)]
        public required string FirstName { get; set; }
        [Required, MaxLength(50)]
        public required string LastName { get; set; }
        public int Age { get; set; }
        [Phone]
        public required string PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public required string Address { get; set; }
        public required string JobTitle { get; set; }
        public required decimal Salary { get; set; }
        public int? DepartmentId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
