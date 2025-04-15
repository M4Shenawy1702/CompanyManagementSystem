using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Department
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public required string Name { get; set; }
        [Required, MaxLength(250)]
        public required string Description { get; set; }
        [Required]
        public int? ManagerId { get; set; }
        public virtual Employee Manager { get; set; } = null!;

        public virtual List<Employee> Employees { get; set; } = [];
        public bool IsDeleted { get; set; }
        public DateTime EstablishedDate { get; set; }
    }
}
