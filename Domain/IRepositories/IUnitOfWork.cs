using Domain.Entities;

namespace Domain.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IDepartmentRepository Departments { get; }
        IProjectRepository Projects { get; }
        IHolidayRepository Holidays { get; }
        IPayrollRepository Payrolls { get; }
        IUserRepository Users { get; }
        IGenericRepository<Employee> Employees { get; }
        //int Complete();
        Task<int> CompleteAsync();
    }
}
