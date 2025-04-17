using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public virtual Employee? Employee { get; set; }
        public virtual Admin? Admin { get; set; }
        public virtual ICollection<Payroll>? Payrolls { get; set; } = new List<Payroll>();
        public string ProfileImg { get; set; } = string.Empty;
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public bool IsDeleted { get; set; }
    }
    public enum Gender
    {
        Male,
        Female
    }
}
