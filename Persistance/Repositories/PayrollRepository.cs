using Domain.Entities;
using Domain.IRepositories;
using Persistance.DbContext;

namespace Persistance.Repositories
{
    public class PayrollRepository : GenericRepository<Payroll>, IPayrollRepository
    {
        public PayrollRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
