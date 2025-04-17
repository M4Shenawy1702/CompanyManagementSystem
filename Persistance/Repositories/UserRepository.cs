using Domain.Dtos.AuthDots;
using Domain.Dtos.EmployeeDto;
using Domain.Entities;
using Domain.IRepositories;
using Domain.IServices;
using Microsoft.AspNetCore.Identity;
using Persistance.DbContext;
using System.IdentityModel.Tokens.Jwt;

namespace Persistance.Repositories
{
    public class UserRepository : GenericRepository<ApplicationUser>, IUserRepository
    {
        private readonly IJWTTokenGenerator _jwtTokenGenerator;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private List<string> _AllowedExtensions = new List<string> { ".jpg", ".png" };
        private long _MaxAllowedSize = 10485760;

        public UserRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IJWTTokenGenerator jwtTokenGenerator) : base(context)
        {
            _context = context;
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        public async Task<ApplicationUser> GetUserById(string id) => await _userManager.FindByIdAsync(id);

        public async Task<AuthModel> AddUser(AddUserDto dto)
        {
            if (await _userManager.FindByEmailAsync(dto.Email) is not null)
                return new AuthModel { Message = "Email is already registered!" };

            if (await _userManager.FindByNameAsync(dto.UserName) is not null)
                return new AuthModel { Message = "Username is already registered!" };

            string imagePath = string.Empty;

            if (dto.ProfileImg is not null)
            {
                var extension = Path.GetExtension(dto.ProfileImg.FileName).ToLower();
                if (!_AllowedExtensions.Contains(extension))
                    return new AuthModel { Message = "Only .jpg and .png are allowed" };

                if (dto.ProfileImg.Length > _MaxAllowedSize)
                    return new AuthModel { Message = "Max allowed size is 10MB" };

                var imageName = $"{Guid.NewGuid()}{extension}";
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/users", imageName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await dto.ProfileImg.CopyToAsync(stream);
                }

                imagePath = $"/images/users/{imageName}";
            }

            var user = new ApplicationUser
            {
                UserName = dto.UserName,
                Email = dto.Email,
                JobTitle = dto.JobTitle,
                Address = dto.Address,
                Age = dto.Age,
                PhoneNumber = dto.PhoneNumber,
                Gender = dto.Gender,
                Salary = dto.Salary,
                ProfileImg = imagePath
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new AuthModel { Message = errors };
            }

            try
            {
                switch (dto.Role)
                {
                    case UserRoles.Admin:
                        var admin = new Admin
                        {
                            UserId = user.Id,
                        };
                        await _context.Admins.AddAsync(admin);
                        break;

                    case UserRoles.Employee:
                        var employee = new Employee
                        {
                            UserId = user.Id,
                        };
                        await _context.Employees.AddAsync(employee);
                        break;

                    default:
                        return new AuthModel { Message = "Invalid Role!" };
                }


                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new AuthModel { Message = $"An error occurred while saving user details: {ex.Message}" };
            }

            await _userManager.AddToRoleAsync(user, dto.Role.ToString());

            var jwtSecurityToken = await _jwtTokenGenerator.CreateJwtTokenAsync(user);

            return new AuthModel
            {
                Email = user.Email,
                ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Roles = new List<string> { dto.Role.ToString() },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Username = user.UserName
            };
        }


        public async Task<ApplicationUser> UpdateInfo(string id, UpdateInfoDto dto)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) throw new Exception("User Not Found");

            if (dto.Email != user.Email && await _userManager.FindByEmailAsync(dto.Email) is not null)
                throw new Exception("Email Already Exists");

            user.UserName = dto.UserName;
            user.PhoneNumber = dto.PhoneNumber;
            user.Email = dto.Email;
            user.Age = dto.Age;
            user.Gender = dto.Gender;
            user.Salary = dto.Salary;

            if (dto.ProfileImg is not null)
            {
                var extension = Path.GetExtension(dto.ProfileImg.FileName).ToLower();
                if (!_AllowedExtensions.Contains(extension))
                    throw new Exception("Only .jpg and .png are allowed");

                if (dto.ProfileImg.Length > _MaxAllowedSize)
                    throw new Exception("Max allowed size is 10MB");

                var imageName = $"{Guid.NewGuid()}{extension}";
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/users", imageName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await dto.ProfileImg.CopyToAsync(stream);
                }

                user.ProfileImg = $"/images/users/{imageName}";
            }

            await _context.SaveChangesAsync();

            return user;
        }

    }
}
