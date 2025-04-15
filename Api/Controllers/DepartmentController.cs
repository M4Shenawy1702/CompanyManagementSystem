using Domain.Dtos.Department;
using Domain.Dtos.DepartmentDots;
using Domain.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpPost("AddDepartment")]
        public async Task<ActionResult<DepartmentResultDto>> AddDepartment([FromForm] DepartmentDto Dto)
        {
            var department = await _departmentService.AddDepartment(Dto);

            return Ok(department);
        }
        [HttpGet("GetAllDepartment")]
        public async Task<ActionResult<IEnumerable<DepartmentResultDto>>> GetAllDepartments()
        {
            var departments = await _departmentService.GetAllDepartment();

            return Ok(departments);
        }
        [HttpGet("GetDepartmentById/{depId}")]
        public async Task<ActionResult<DepartmentResultDto>> GetDepartmentById(int depId)
        {
            var department = await _departmentService.GetDepartmentById(depId);

            return Ok(department);
        }
        [HttpGet("GetDepartmentByName/{depName}")]
        public async Task<ActionResult<DepartmentResultDto>> GetDepartmentByName(string depName)
        {
            var department = await _departmentService.GetDepartmentByName(depName);

            return Ok(department);
        }
        [HttpPut("ToggleDepartment/{Id}")]
        public async Task<ActionResult<DepartmentResultDto>> ToggleDepartment(int Id)
        {
            var result = await _departmentService.ToggleDepartment(Id);

            return Ok(result);
        }
        [HttpPut("UpdateDepartment/{Id}")]
        public async Task<ActionResult<DepartmentResultDto>> UpdateDepartment([FromForm] DepartmentDto Dto, int Id)
        {
            var department = await _departmentService.UpdateDepartment(Dto, Id);

            return Ok(department);
        }
        [HttpPut("ChangeDeptManager/{Id}")]
        public async Task<ActionResult<DepartmentResultDto>> ChangeDeptManager([FromForm] Emp_DeptDto Dto, int Id)
        {
            var department = await _departmentService.ChangeDeptManager(Dto, Id);

            return Ok(department);
        }
        [HttpPut("AddEmpToDept/{Id}")]
        public async Task<ActionResult<DepartmentResultDto>> AddEmpToDept([FromForm] Emp_DeptDto Dto, int Id)
        {
            var department = await _departmentService.AddEmpToDept(Dto, Id);

            return Ok(department);
        }
        [HttpPut("RemoveEmpFromDept/{Id}")]
        public async Task<ActionResult<DepartmentResultDto>> RemoveEmpFromDept([FromForm] Emp_DeptDto Dto, int Id)
        {
            var department = await _departmentService.RemoveEmpFromDept(Dto, Id);

            return Ok(department);
        }
    }
}
