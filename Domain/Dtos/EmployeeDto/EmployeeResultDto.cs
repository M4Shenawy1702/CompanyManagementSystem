using Domain.Entities;

namespace Domain.Dtos.EmployeeDto
{
    public class EmployeeResultDto
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public byte[]? ProfileImg { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public int Age { get; set; }
        public string? PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public string? Address { get; set; }
        public string? JobTitle { get; set; }
        public decimal Salary { get; set; }
        public int? DepartmentId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
