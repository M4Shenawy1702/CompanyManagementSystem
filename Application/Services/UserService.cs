using AutoMapper;
using Domain.Dtos.AuthDots;
using Domain.Dtos.EmployeeDto;
using Domain.Errors;
using Domain.IRepositories;
using Domain.IServices;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AuthModel> Add(AddUserDto Dto)
        {
            var result = await _unitOfWork.Users.AddUser(Dto);
            await _unitOfWork.CompleteAsync();

            return result;
        }

        public async Task<AuthModel> Toggle(int id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id)
                           ?? throw new NotFoundException("Employee not found.", "EMPLOYEE_NOT_FOUND");

            user.IsDeleted = !user.IsDeleted;
            _unitOfWork.Users.Update(user);
            await _unitOfWork.CompleteAsync();

            var result = new AuthModel
            {
                IsAuthenticated = true,
                Message = "Employee deleted successfully.",
                Email = user.Email
            };
            return result;
        }

        public async Task<UserResultDto> Get(string id)
        {
            var User = await _unitOfWork.Users.GetUserById(id)
                           ?? throw new NotFoundException("Employee not found.", "EMPLOYEE_NOT_FOUND");

            return _mapper.Map<UserResultDto>(User);
        }

        public async Task<List<UserResultDto>> GetAll()
        {
            var employees = await _unitOfWork.Users.GetAllAsync();

            return _mapper.Map<List<UserResultDto>>(employees);
        }

        public async Task<AuthModel> Update(UpdateInfoDto Dto, string id)
        {
            var updatedUser = await _unitOfWork.Users.UpdateInfo(id, Dto)
                              ?? throw new NotFoundException("Employee not found for update.", "EMPLOYEE_NOT_FOUND");

            _unitOfWork.Users.Update(updatedUser);
            await _unitOfWork.CompleteAsync();

            var result = new AuthModel
            {
                IsAuthenticated = true,
                Message = "Employee updated successfully.",
                Email = updatedUser.Email
            };
            return result;
        }

    }
}
