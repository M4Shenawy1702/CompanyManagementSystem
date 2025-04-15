using AutoMapper;
using Domain.Dtos.AuthDots;
using Domain.Dtos.EmployeeDto;
using Domain.Errors;
using Domain.IRepositories;
using Domain.IServices;

namespace Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AuthModel> Add(RegisterDto Dto)
        {
            var result = await _unitOfWork.Employees.AddEmployee(Dto, "Employee");
            await _unitOfWork.CompleteAsync();

            return result;
        }

        public async Task<EmployeeResultDto> Toggle(int id)
        {
            var employee = await _unitOfWork.Employees.GetByIdAsync(id)
                           ?? throw new NotFoundException("Employee not found.", "EMPLOYEE_NOT_FOUND");

            employee.IsDeleted = !employee.IsDeleted;
            _unitOfWork.Employees.Update(employee);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<EmployeeResultDto>(employee);
        }

        public async Task<EmployeeResultDto> Get(int id)
        {
            var employee = await _unitOfWork.Employees.GetByIdAsync(id)
                           ?? throw new NotFoundException("Employee not found.", "EMPLOYEE_NOT_FOUND");

            return _mapper.Map<EmployeeResultDto>(employee);
        }

        public async Task<List<EmployeeResultDto>> GetAll()
        {
            var employees = await _unitOfWork.Employees.GetAllAsync();
            return _mapper.Map<List<EmployeeResultDto>>(employees);
        }

        public async Task<EmployeeResultDto> Update(UpdateInfoDto Dto, int id)
        {
            var result = await _unitOfWork.Employees.UpdateInfo(id, Dto)
                         ?? throw new NotFoundException("Employee not found for update.", "EMPLOYEE_NOT_FOUND");

            _unitOfWork.Employees.Update(result);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<EmployeeResultDto>(result);
        }
    }
}
