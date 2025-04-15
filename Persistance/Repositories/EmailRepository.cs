using Domain.Entities;
using Domain.IRepositories;
using Persistance.DbContext;

namespace Persistance.Repositories
{
    public class EmailRepository : GenericRepository<Email>, IEmailRepository
    {
        public EmailRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
