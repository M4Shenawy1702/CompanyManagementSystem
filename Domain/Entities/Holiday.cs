using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Holiday
    {
        public int Id { get; set; }
        [Required,MaxLength(50)]
        public required string Name { get; set; }
        [Required, MaxLength(250)]
        public required string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? AdminId { get; set; }//remove it 
        public bool IsDeleted { get; set; }
    }
}
