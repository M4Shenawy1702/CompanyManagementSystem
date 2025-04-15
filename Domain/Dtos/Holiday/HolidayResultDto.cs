using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Holiday
{
    public class HolidayResultDto
    {
        public required int Id { get; set; }
        [Required, MaxLength(50)]
        public required string Name { get; set; }
        [Required, MaxLength(250)]
        public required string Description { get; set; }
        public required DateTime? StartDate { get; set; }
        public required DateTime? EndDate { get; set; }
        public required DateTime? ModifiedDate { get; set; }
        public required int? AdminId { get; set; }
        public required bool IsDeleted { get; set; }
    }
}
