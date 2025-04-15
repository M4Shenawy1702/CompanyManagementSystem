using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.EmployeeDto
{
    public class UpdateInfoDto
    {

        [Required, MaxLength(50)]
        public required string FirstName { get; set; }
        [Required, MaxLength(50)]
        public required string LastName { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
        public required string UserName { get; set; }
        public required IFormFile? ProfileImg { get; set; }
        public required int Age { get; set; }
        [Phone]
        public required string? PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public string? Address { get; set; }
        public string? JobTitle { get; set; }
    }
}
