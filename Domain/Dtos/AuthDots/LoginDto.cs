using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.AuthDots
{
    public class LoginDto
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}