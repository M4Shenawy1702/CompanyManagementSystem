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
        [HttpGet("GetAllEmployees")]
        public async Task<ActionResult<IEnumerable<UserResultDto>>> GetAllEmployees()
        {
            var employees = await _employeeService.GetAll();
            return Ok(employees);
        }
        [HttpGet("GetEmployeeById/{id}")]
        public async Task<ActionResult<UserResultDto>> GetEmployeeById(string id)
        {
            var employee = await _employeeService.Get(id);
            return Ok(employee);
        }
        [HttpPost("AddEmployee")]
        public async Task<ActionResult<AuthModel>> AddEmployee([FromForm] AddUserDto Dto)
        {
            var employee = await _employeeService.Add(Dto);
            return Ok(employee);
        }
        [HttpPut("UpdateEmployee/{id}")]
        public async Task<ActionResult<UserResultDto>> UpdateEmployee([FromForm] UpdateInfoDto Dto, string id)
        {
            var employee = await _employeeService.Update(Dto, id);
            return Ok(employee);
        }
        [HttpPut("ToggleEmployee/{id}")]
        public async Task<ActionResult<UserResultDto>> ToggleEmployee(int id)
        {
            var employee = await _employeeService.Toggle(id);
            return Ok(employee);
        }
    }
}
