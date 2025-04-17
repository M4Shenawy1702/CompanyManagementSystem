using Domain.Dtos.AuthDots;
using Domain.Dtos.EmployeeDto;
using Domain.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _employeeService;
        public UserController(IUserService employeeService)
        {
            _employeeService = employeeService;
        }
        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<IEnumerable<UserResultDto>>> GetAllUsers()
        {
            var employees = await _employeeService.GetAll();
            return Ok(employees);
        }
        [HttpGet("GetUserById/{id}")]
        public async Task<ActionResult<UserResultDto>> GetUserById(string id)
        {
            var employee = await _employeeService.Get(id);
            return Ok(employee);
        }
        [HttpPost("AddUser")]
        public async Task<ActionResult<AuthModel>> AddUser([FromForm] AddUserDto Dto)
        {
            var employee = await _employeeService.Add(Dto);
            return Ok(employee);
        }
        [HttpPut("UpdateUser/{id}")]
        public async Task<ActionResult<UserResultDto>> UpdateUser([FromForm] UpdateInfoDto Dto, string id)
        {
            var employee = await _employeeService.Update(Dto, id);
            return Ok(employee);
        }
        [HttpPut("ToggleUser/{id}")]
        public async Task<ActionResult<UserResultDto>> ToggleUser(int id)
        {
            var employee = await _employeeService.Toggle(id);
            return Ok(employee);
        }
    }
}
