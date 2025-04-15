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

        //public async Task<AuthModel> RegisterAsync(RegisterDto model, string Role)
        //{

        //    if (await _userManager.FindByEmailAsync(model.Email) is not null)
        //        return new AuthModel { Message = "Email is already registered!" };

        //    if (await _userManager.FindByNameAsync(model.UserName) is not null)
        //        return new AuthModel { Message = "Username is already registered!" };

        //    // Validate and process profile image
        //    byte[] profileImageBytes;
        //    try
        //    {
        //        profileImageBytes = await ValidateAndProcessProfileImageAsync(model.ProfileImg);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new AuthModel { Message = ex.Message };
        //    }

        //    var user = new ApplicationUser
        //    {
        //        UserName = model.UserName,
        //        Email = model.Email,

        //    };

        //    var result = await _userManager.CreateAsync(user, model.Password);
        //    if (!result.Succeeded)
        //    {
        //        var errors = string.Join(", ", result.Errors.Select(e => e.Description));
        //        return new AuthModel { Message = errors };
        //    }

        //    try
        //    {
        //        if (Role == "Employee")
        //        {
        //            var employee = new Employee
        //            {
        //                UserId = user.Id,
        //                JobTitle = model.JobTitle,
        //                ProfileImg = profileImageBytes,
        //                FirstName = model.FirstName,
        //                LastName = model.LastName,
        //                Address = model.Address,
        //                Age = model.Age,
        //                PhoneNumber = model.PhoneNumber,
        //                Gender = model.Gender
        //            };

        //            await _context.Employees.AddAsync(employee);
        //        }
        //        else if (Role == "Admin")
        //        {
        //            var admin = new Admin
        //            {
        //                UserId = user.Id,
        //                ProfileImg = profileImageBytes
        //            };

        //            await _context.Admins.AddAsync(admin);
        //        }

        //        await _context.SaveChangesAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        return new AuthModel { Message = $"An error occurred while saving user details: {ex.Message}" };
        //    }

        //    if (!result.Succeeded)
        //    {
        //        var errors = string.Empty;

        //        foreach (var error in result.Errors)
        //            errors += $"{error.Description},";

        //        return new AuthModel { Message = errors };
        //    }

        //    await _userManager.AddToRoleAsync(user, Role);

        //    var jwtSecurityToken = await CreateJwtToken(user);


        //    return new AuthModel
        //    {
        //        Email = user.Email,
        //        ExpiresOn = jwtSecurityToken.ValidTo,
        //        IsAuthenticated = true,
        //        Roles = new List<string> { Role },
        //        Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
        //        Username = user.UserName,

        //    };
        //}

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