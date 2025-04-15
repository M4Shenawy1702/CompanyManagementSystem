using Domain.Dtos.AuthDots;
using Domain.Dtos.EmployeeDto;
using Domain.Entities;

namespace Domain.IRepositories
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<AuthModel> AddEmployee(RegisterDto model, string Role);
        Task<Employee> UpdateInfo(int id, UpdateInfoDto dto);
    }
}
