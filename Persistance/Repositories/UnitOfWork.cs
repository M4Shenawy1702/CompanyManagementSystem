using Domain.Entities;
using Domain.IRepositories;
using Domain.IServices;
using Domain.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Persistance.DbContext;

namespace Persistance.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJWTTokenGenerator _jwtTokenGenerator;
        private readonly JWT _jwt;


        public IDepartmentRepository Departments { get; private set; }
        public IEmployeeRepository Employees { get; private set; }
        public IProjectRepository Projects { get; private set; }
        public IHolidayRepository Holidays { get; private set; }
        public IEmailRepository Emails { get; private set; }
        public IPayrollRepository Payrolls { get; private set; }

        public UnitOfWork(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IOptions<JWT> jwt,
            IJWTTokenGenerator jwtTokenGenerator
            //IEmailService emailService
            )
        {
            _context = context;
            _userManager = userManager;
            _jwt = jwt.Value;
            _jwtTokenGenerator = jwtTokenGenerator;

            Projects = new ProjectRepository(_context);
            Departments = new DepartmentRepository(_context);
            Employees = new EmployeeRepository(_context, _userManager, jwtTokenGenerator);
            Holidays = new HolidayRepository(_context);
            Emails = new EmailRepository(_context);
            Payrolls = new PayrollRepository(_context);
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public Task<int> CompleteAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
