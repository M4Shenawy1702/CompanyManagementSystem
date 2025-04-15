using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.EmailDto;

public class EmailDto
{
    [Required, EmailAddress]
    public string From { get; set; }
    [Required, EmailAddress]
    public string To { get; set; }
    [Required]
    public string Subject { get; set; }
    [Required]
    public string Body { get; set; }
}
