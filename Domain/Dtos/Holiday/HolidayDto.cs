using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Holiday
{
    public class HolidayDto
    {
        [Required, MaxLength(50)]
        public required string Name { get; set; }
        [Required, MaxLength(250)]
        public required string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? AdminId { get; set; }
    }
}
