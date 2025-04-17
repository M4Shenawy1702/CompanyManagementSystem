using Domain.Entities;

namespace Domain.Dtos.EmployeeDto
{
    public class UserResultDto
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string ProfileImg { get; set; }
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
