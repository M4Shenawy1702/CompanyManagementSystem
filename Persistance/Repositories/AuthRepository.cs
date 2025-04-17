using Domain.Dtos.AuthDots;
using Domain.Entities;
using Domain.IRepositories;
using Domain.IServices;
using Domain.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Persistance.DbContext;
using System.IdentityModel.Tokens.Jwt;


namespace Persistance.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JWT _jwt;
        private readonly IJWTTokenGenerator _jwtTokenGenerator;
        public AuthRepository(UserManager<ApplicationUser> userManager,
            IOptions<JWT> jwt, ApplicationDbContext context, IJWTTokenGenerator jwtTokenGenerator)
        {
            _userManager = userManager;
            _jwt = jwt.Value;
            _context = context;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<AuthModel> GetTokenAsync(LoginDto model)
        {
            var authModel = new AuthModel();

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authModel.Message = "Email or Password is incorrect!";
                return authModel;
            }

            var jwtSecurityToken = await _jwtTokenGenerator.CreateJwtTokenAsync(user); ;
            var rolesList = await _userManager.GetRolesAsync(user);

            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Email = user.Email;
            authModel.Username = user.UserName;
            authModel.ExpiresOn = jwtSecurityToken.ValidTo;
            authModel.Roles = rolesList.ToList();


            return authModel;
        }
        public async Task<AuthModel> Delete(string UserId)
        {
            var authModel = new AuthModel();

            var user = await _userManager.FindByIdAsync(UserId);
            if (user is null)
            {
                authModel.Message = "User Not Found!";
                return authModel;
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                authModel.Message = " Failed to delete the user";
            }

            authModel.Message = "User deleted successfully";

            return new AuthModel
            {
                Email = user.Email,
                Username = user.UserName,
                Message = authModel.Message
            };
        }



    }
}