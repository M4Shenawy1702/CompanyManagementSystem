using Domain.Dtos.AuthDots;
using Domain.Dtos.EmployeeDto;

namespace Domain.IServices
{
    public interface IUserService
    {
        public Task<List<UserResultDto>> GetAll();
        public Task<UserResultDto> Get(string id);
        public Task<AuthModel> Add(AddUserDto Dto);
        public Task<AuthModel> Update(UpdateInfoDto Dto, string id);
        public Task<AuthModel> Toggle(int id);
    }
}
