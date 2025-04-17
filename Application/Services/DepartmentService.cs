using AutoMapper;
using Domain.Dtos.Department;
using Domain.Dtos.DepartmentDots;
using Domain.Entities;
using Domain.Errors;
using Domain.IRepositories;
using Domain.IServices;

namespace Application.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DepartmentResultDto>> GetAllDepartment()
        {
            var departments = await _unitOfWork.Departments.GetAllAsync();
            var departmentResultDto = _mapper.Map<IEnumerable<DepartmentResultDto>>(departments);
            return departmentResultDto;
        }

        public async Task<DepartmentResultDto> AddDepartment(DepartmentDto Dto)
        {
            var manager = await _unitOfWork.Employees.GetByIdAsync(Dto.ManagerId) ?? throw new NotFoundException("Manager not found.");

            if (await _unitOfWork.Departments.FindAsync(d => d.ManagerId == Dto.ManagerId && !d.IsDeleted) is not null)
                throw new ServiceException(400, "The manager is already assigned to an existing department.", "MANAGER_ALREADY_ASSIGNED");

            var department = _mapper.Map<Department>(Dto);
            var result = await _unitOfWork.Departments.InsertAsync(department);
            await _unitOfWork.CompleteAsync();

            var departmentResultDto = _mapper.Map<DepartmentResultDto>(result);
            return departmentResultDto;
        }

        public async Task<DepartmentResultDto> GetDepartmentById(int deptId)
        {
            var department = await _unitOfWork.Departments.GetByIdAsync(deptId) ?? throw new NotFoundException("Department not found.", "DEPARTMENT_NOT_FOUND");
            var departmentResultDto = _mapper.Map<DepartmentResultDto>(department);
            return departmentResultDto;
        }

        public async Task<DepartmentResultDto> GetDepartmentByName(string deptName)
        {
            var department = await _unitOfWork.Departments.FindAsync(d => d.Name.Equals(deptName, StringComparison.OrdinalIgnoreCase));
            if (department is null)
            {
                throw new NotFoundException("Department not found with the provided name.", "DEPARTMENT_NOT_FOUND");
            }
            var departmentResultDto = _mapper.Map<DepartmentResultDto>(department);
            return departmentResultDto;
        }

        public async Task<DepartmentResultDto> ToggleDepartment(int deptId)
        {
            var department = await _unitOfWork.Departments.GetByIdAsync(deptId) ?? throw new NotFoundException("Department not found.", "DEPARTMENT_NOT_FOUND");

            department.IsDeleted = !department.IsDeleted;
            await _unitOfWork.CompleteAsync();

            var departmentResultDto = _mapper.Map<DepartmentResultDto>(department);
            return departmentResultDto;
        }

        public async Task<DepartmentResultDto> UpdateDepartment(DepartmentDto Dto, int deptId)
        {
            if (await _unitOfWork.Departments.FindAsync(d => d.ManagerId == Dto.ManagerId && !d.IsDeleted) is not null)
                throw new ServiceException(400, "Manager is already assigned to an existing department.", "MANAGER_ALREADY_ASSIGNED");

            var department = await _unitOfWork.Departments.GetByIdAsync(deptId) ?? throw new NotFoundException("Department not found.", "DEPARTMENT_NOT_FOUND");

            department.Name = Dto.Name;
            department.Description = Dto.Description;

            await _unitOfWork.CompleteAsync();

            var departmentResultDto = _mapper.Map<DepartmentResultDto>(department);
            return departmentResultDto;
        }

        public async Task<DepartmentResultDto> ChangeDeptManager(Emp_DeptDto Dto, int DeptId)
        {
            var employee = await _unitOfWork.Employees.GetByIdAsync(Dto.EmpId) ?? throw new NotFoundException("Employee not found.", "EMPLOYEE_NOT_FOUND");

            if (await _unitOfWork.Departments.FindAsync(d => d.ManagerId == Dto.EmpId && !d.IsDeleted) is not null)
                throw new ServiceException(400, "Manager is already assigned to an existing department.", "MANAGER_ALREADY_ASSIGNED");

            var department = await _unitOfWork.Departments.GetByIdAsync(DeptId) ?? throw new NotFoundException("Department not found.", "DEPARTMENT_NOT_FOUND");

            if (department.ManagerId != Dto.EmpId)
            {
                department.ManagerId = Dto.EmpId;
                await _unitOfWork.CompleteAsync();
            }
            else
            {
                throw new ServiceException(400, "The employee is already the manager of this department.", "EMPLOYEE_ALREADY_MANAGER");
            }

            var departmentResultDto = _mapper.Map<DepartmentResultDto>(department);
            return departmentResultDto;
        }

        public async Task<DepartmentResultDto> AddEmpToDept(Emp_DeptDto Dto, int DeptId)
        {
            var department = await _unitOfWork.Departments.GetByIdAsync(DeptId) ?? throw new NotFoundException("Department not found.", "DEPARTMENT_NOT_FOUND");
            var employee = await _unitOfWork.Employees.GetByIdAsync(Dto.EmpId) ?? throw new NotFoundException("Employee not found.", "EMPLOYEE_NOT_FOUND");

            if (!department.Employees!.Contains(employee))
            {
                department.Employees!.Add(employee);
                await _unitOfWork.CompleteAsync();
            }
            else
            {
                throw new ServiceException(400, "Employee is already part of the department.", "EMPLOYEE_ALREADY_IN_DEPT");
            }

            var departmentResultDto = _mapper.Map<DepartmentResultDto>(department);
            return departmentResultDto;
        }

        public async Task<DepartmentResultDto> RemoveEmpFromDept(Emp_DeptDto Dto, int DeptId)
        {
            var department = await _unitOfWork.Departments.GetByIdAsync(DeptId) ?? throw new NotFoundException("Department not found.", "DEPARTMENT_NOT_FOUND");
            var employee = await _unitOfWork.Employees.GetByIdAsync(Dto.EmpId) ?? throw new NotFoundException("Employee not found.", "EMPLOYEE_NOT_FOUND");

            if (department.Employees!.Contains(employee))
            {
                department.Employees!.Remove(employee);
                await _unitOfWork.CompleteAsync();
            }
            else
            {
                throw new ServiceException(400, "Employee is not part of the department.", "EMPLOYEE_NOT_IN_DEPT");
            }

            var departmentResultDto = _mapper.Map<DepartmentResultDto>(department);
            return departmentResultDto;
        }
    }
}
