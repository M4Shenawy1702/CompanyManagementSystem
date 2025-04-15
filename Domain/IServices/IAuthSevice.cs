using Domain.Dtos.AuthDots;

namespace Domain.IServices
{
    public interface IAuthSevice
    {
        Task<AuthModel> Login(LoginDto model);
    }
}
