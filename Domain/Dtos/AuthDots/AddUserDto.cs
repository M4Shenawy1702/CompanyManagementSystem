using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
namespace Domain.Dtos.AuthDots

{
    public class AddUserDto
    {
        public required IFormFile? ProfileImg { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string UserName { get; set; }
        public required int Age { get; set; }
        [Phone]
        public required string? PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public string? Address { get; set; }
        public string? JobTitle { get; set; }
        public decimal Salary { get; set; }
        public UserRoles Role { get; set; }
    }
    public enum UserRoles
    {
        Admin,
        Employee,
    }
}