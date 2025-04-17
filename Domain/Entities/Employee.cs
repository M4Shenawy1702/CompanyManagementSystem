namespace Domain.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }
        public int? DepartmentId { get; set; }
        public virtual Department? Department { get; set; }

    }
}
