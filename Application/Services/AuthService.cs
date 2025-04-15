using Domain.Dtos.AuthDots;
using Domain.IRepositories;
using Domain.IServices;

namespace Application.Services
{
    public class AuthService : IAuthSevice
    {
        private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }


        public async Task<AuthModel> Login(LoginDto model)
        {
            var result = await _authRepository.GetTokenAsync(model);

            if (!result.IsAuthenticated)
                throw new Exception(result.Message);

            return result;
        }

    }
}
