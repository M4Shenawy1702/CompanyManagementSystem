using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.DepartmentDots
{
    public class DepartmentResultDto
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public required string Name { get; set; }
        [Required, MaxLength(250)]
        public required string Description { get; set; }
        [Required]
        public required int ManagerId { get; set; }
        public required List<string> EmployeesUsernames { get; set; } = [];
        public required bool IsDeleted { get; set; }
        public required DateTime EstablishedDate { get; set; }
    }
}
