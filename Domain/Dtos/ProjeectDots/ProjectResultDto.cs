using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.ProjeectDots
{
    public class ProjectResultDto
    {
        public required int Id { get; set; }
        [Required, MaxLength(50)]
        public required string Name { get; set; }
        [Required, MaxLength(250)]
        public required string Description { get; set; }
        [Required]
        public required DateTime StartDate { get; set; }
        public required DateTime EndDate { get; set; }
        public required bool Status { get; set; }
        public required bool IsDeleted { get; set; }
        public required int ManagerId { get; set; }
        public required List<Employee>? Employees { get; set; } = [];
    }
}
