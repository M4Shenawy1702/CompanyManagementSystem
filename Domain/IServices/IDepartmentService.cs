using Domain.Dtos.Department;
using Domain.Dtos.DepartmentDots;

namespace Domain.IServices
{
    public interface IDepartmentService
    {
        public Task<DepartmentResultDto> AddDepartment(DepartmentDto Dto);
        public Task<DepartmentResultDto> ToggleDepartment(int DeptId);
        public Task<IEnumerable<DepartmentResultDto>> GetAllDepartment();
        public Task<DepartmentResultDto> GetDepartmentById(int DeptId);
        public Task<DepartmentResultDto> GetDepartmentByName(string DeptName);
        public Task<DepartmentResultDto> UpdateDepartment(DepartmentDto Dto, int DeptId);
        public Task<DepartmentResultDto> ChangeDeptManager(Emp_DeptDto Dto, int DeptId);
        public Task<DepartmentResultDto> AddEmpToDept(Emp_DeptDto Dto, int DeptId);
        public Task<DepartmentResultDto> RemoveEmpFromDept(Emp_DeptDto Dto, int DeptId);

    }
}
