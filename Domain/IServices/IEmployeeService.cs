using Domain.Dtos.AuthDots;
using Domain.Dtos.EmployeeDto;

namespace Domain.IServices
{
    public interface IEmployeeService
    {
        public Task<List<EmployeeResultDto>> GetAll();
        public Task<EmployeeResultDto> Get(int id);
        public Task<AuthModel> Add(RegisterDto Dto);
        public Task<EmployeeResultDto> Update(UpdateInfoDto Dto, int id);
        public Task<EmployeeResultDto> Toggle(int id);
    }
}
