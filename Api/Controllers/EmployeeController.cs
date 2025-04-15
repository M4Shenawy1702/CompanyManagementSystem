using Domain.Dtos.AuthDots;
using Domain.Dtos.EmployeeDto;
using Domain.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        [HttpGet("GetAllEmployees")]
        public async Task<ActionResult<IEnumerable<EmployeeResultDto>>> GetAllEmployees()
        {
            var employees = await _employeeService.GetAll();
            return Ok(employees);
        }
        [HttpGet("GetEmployeeById/{id}")]
        public async Task<ActionResult<EmployeeResultDto>> GetEmployeeById(int id)
        {
            var employee = await _employeeService.Get(id);
            return Ok(employee);
        }
        [HttpPost("AddEmployee")]
        public async Task<ActionResult<EmployeeResultDto>> AddEmployee([FromForm] RegisterDto Dto)
        {
            var employee = await _employeeService.Add(Dto);
            return Ok(employee);
        }
        [HttpPut("UpdateEmployee/{id}")]
        public async Task<ActionResult<EmployeeResultDto>> UpdateEmployee([FromForm] UpdateInfoDto Dto, int id)
        {
            var employee = await _employeeService.Update(Dto, id);
            return Ok(employee);
        }
        [HttpPut("ToggleEmployee/{id}")]
        public async Task<ActionResult<EmployeeResultDto>> ToggleEmployee(int id)
        {
            var employee = await _employeeService.Toggle(id);
            return Ok(employee);
        }
    }
}
