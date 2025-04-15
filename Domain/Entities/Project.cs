using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Project
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public required string Name { get; set; }
        [Required, MaxLength(250)]
        public required string Description { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Status { get; set; }
        public bool IsDeleted { get; set; }
        public int ManagerId { get; set; }
        public virtual Employee? Manager { get; set; }
        public virtual List<Employee>? Employees { get; set; }
    }
}
