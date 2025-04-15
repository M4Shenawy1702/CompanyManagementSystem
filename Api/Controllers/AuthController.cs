using Domain.Dtos.AuthDots;
using Domain.Entities;
using Domain.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace OrderManagementSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthSevice _authSevice;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthController(UserManager<ApplicationUser> userManager, IAuthSevice authSevice)
        {
            _userManager = userManager;
            _authSevice = authSevice;
        }

        [HttpPost("LogIn")]
        public async Task<ActionResult<AuthModel>> LogIn([FromForm] LoginDto Dto)
        {
            var result = await _authSevice.Login(Dto);

            return Ok(result);
        }
    }
}
