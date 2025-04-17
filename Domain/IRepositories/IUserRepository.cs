using Domain.Dtos.AuthDots;
using Domain.Dtos.EmployeeDto;
using Domain.Entities;

namespace Domain.IRepositories
{
    public interface IUserRepository : IGenericRepository<ApplicationUser>
    {
        Task<ApplicationUser> GetUserById(string id);
        Task<AuthModel> AddUser(AddUserDto dto);
        Task<ApplicationUser> UpdateInfo(string id, UpdateInfoDto dto);
    }
}
