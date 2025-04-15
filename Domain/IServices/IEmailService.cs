using Domain.Dtos.EmailDto;

namespace MoviesManagementSystem.Core.IServices
{
    public interface IEmailService
    {
        Task<EmailResultDto> SendEmailAsync(EmailDto dto);
        Task<List<EmailResultDto>> GetAllEmailsAsync();
    }

}
