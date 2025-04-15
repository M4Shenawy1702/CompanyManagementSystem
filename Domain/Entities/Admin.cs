
namespace Domain.Entities
{
    public class Admin
    {
        public int Id { get; set; }
        public byte[]? ProfileImg { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }
    }
}
