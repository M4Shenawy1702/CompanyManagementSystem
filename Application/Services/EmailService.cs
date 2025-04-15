using AutoMapper;
using Domain.Dtos.EmailDto;
using Domain.Entities;
using Domain.IRepositories;
using Microsoft.Extensions.Configuration;
using MoviesManagementSystem.Core.IServices;
using System.Net;
using System.Net.Mail;

namespace Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public EmailService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _config = config;
        }

        public async Task<EmailResultDto> SendEmailAsync(EmailDto dto)
        {
            var email = _mapper.Map<Email>(dto);

            try
            {
                var smtpClient = new SmtpClient(_config["Smtp:Host"])
                {
                    Port = int.Parse(_config["Smtp:Port"]!),
                    Credentials = new NetworkCredential(_config["Smtp:Username"], _config["Smtp:Password"]),
                    EnableSsl = true,
                };

                var mail = new MailMessage
                {
                    From = new MailAddress(dto.From!),
                    Subject = dto.Subject,
                    Body = dto.Body,
                    IsBodyHtml = true,
                };
                mail.To.Add(dto.To);

                await smtpClient.SendMailAsync(mail);
                email.IsSent = true;
            }
            catch (Exception ex)
            {
                email.IsSent = false;
                email.ErrorMessage = ex.Message;
            }

            await _unitOfWork.Emails.InsertAsync(email);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<EmailResultDto>(email);
        }

        public async Task<List<EmailResultDto>> GetAllEmailsAsync()
        {
            var emails = await _unitOfWork.Emails.GetAllAsync();
            return _mapper.Map<List<EmailResultDto>>(emails);
        }
    }

}
