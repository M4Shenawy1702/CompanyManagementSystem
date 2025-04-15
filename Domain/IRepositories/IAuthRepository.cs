using Domain.Dtos.AuthDots;

namespace Domain.IRepositories
{
    public interface IAuthRepository
    {
        Task<AuthModel> GetTokenAsync(LoginDto model);
        Task<AuthModel> Delete(string UserId);
    }
}