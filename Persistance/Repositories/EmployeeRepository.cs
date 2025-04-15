using Domain.Dtos.AuthDots;
using Domain.Dtos.EmployeeDto;
using Domain.Entities;
using Domain.IRepositories;
using Domain.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistance.DbContext;
using System.IdentityModel.Tokens.Jwt;

namespace Persistance.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly IJWTTokenGenerator _jwtTokenGenerator;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private List<string> _AllowedExtensions = new List<string> { ".jpg", ".png" };
        private long _MaxAllowedSize = 10485760;

        public EmployeeRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IJWTTokenGenerator jwtTokenGenerator) : base(context)
        {
            _context = context;
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        public async Task<AuthModel> AddEmployee(RegisterDto model, string Role)
        {

            if (await _userManager.FindByEmailAsync(model.Email) is not null)
                return new AuthModel { Message = "Email is already registered!" };

            if (await _userManager.FindByNameAsync(model.UserName) is not null)
                return new AuthModel { Message = "Username is already registered!" };

            // Validate and process profile image
            byte[] profileImageBytes;
            try
            {
                profileImageBytes = await ValidateAndProcessProfileImageAsync(model.ProfileImg);
            }
            catch (Exception ex)
            {
                return new AuthModel { Message = ex.Message };
            }

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,

            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new AuthModel { Message = errors };
            }

            try
            {
                var employee = new Employee
                {
                    UserId = user.Id,
                    JobTitle = model.JobTitle,
                    ProfileImg = profileImageBytes,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Address = model.Address,
                    Age = model.Age,
                    PhoneNumber = model.PhoneNumber,
                    Gender = model.Gender
                };

                await _context.Employees.AddAsync(employee);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new AuthModel { Message = $"An error occurred while saving user details: {ex.Message}" };
            }

            if (!result.Succeeded)
            {
                var errors = string.Empty;

                foreach (var error in result.Errors)
                    errors += $"{error.Description},";

                return new AuthModel { Message = errors };
            }

            await _userManager.AddToRoleAsync(user, Role);

            var jwtSecurityToken = await _jwtTokenGenerator.CreateJwtTokenAsync(user);

            return new AuthModel
            {
                Email = user.Email,
                ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Roles = new List<string> { Role },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Username = user.UserName,

            };
        }
        public async Task<Employee> UpdateInfo(int id, UpdateInfoDto dto)
        {

            var employee = await _context.Employees.SingleOrDefaultAsync(x => x.Id == id);
            if (employee is null) throw new Exception("Employee Not Found");

            var user = await _userManager.FindByIdAsync(employee.UserId);
            if (user == null) throw new Exception("User Not Found");

            if (dto.Email != user.Email && await _userManager.FindByEmailAsync(dto.Email) is not null)
                throw new Exception("Email Already Exists");

            using var dataStream = new MemoryStream();
            await dto.ProfileImg.CopyToAsync(dataStream);


            user.UserName = dto.UserName;
            user.PhoneNumber = dto.PhoneNumber;
            user.Email = dto.Email;
            employee.FirstName = dto.FirstName;
            employee.LastName = dto.LastName;
            employee.Age = dto.Age;
            employee.Gender = dto.Gender;
            employee.ProfileImg = dataStream.ToArray();
            if (dto.ProfileImg is not null)
            {
                var extension = Path.GetExtension(dto.ProfileImg.FileName);
                if (!_AllowedExtensions.Contains(extension.ToLower()))
                    throw new Exception("only .jpg and .png img are allowed");
                if (dto.ProfileImg.Length > _MaxAllowedSize)
                    throw new Exception("Max Allowed Size is 10Mb");

                employee.ProfileImg = dataStream.ToArray();

            }

            await _context.SaveChangesAsync();

            return employee;
        }
    }
}
