using Domain.Dtos.EmailDto;
using Microsoft.AspNetCore.Mvc;
using MoviesManagementSystem.Core.IServices;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromBody] EmailDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _emailService.SendEmailAsync(dto);
            if (!result.IsSent)
                return StatusCode(500, result);

            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllEmails()
        {
            var result = await _emailService.GetAllEmailsAsync();
            return Ok(result);
        }
    }
}
