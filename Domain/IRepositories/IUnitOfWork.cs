namespace Domain.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IDepartmentRepository Departments { get; }
        IEmployeeRepository Employees { get; }
        IProjectRepository Projects { get; }
        IHolidayRepository Holidays { get; }
        IEmailRepository Emails { get; }
        IPayrollRepository Payrolls { get; }
        //int Complete();
        Task<int> CompleteAsync();
    }
}
