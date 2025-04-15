using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public virtual Employee? Employee { get; set; }
        public virtual Admin? Admin { get; set; }
    }
}
