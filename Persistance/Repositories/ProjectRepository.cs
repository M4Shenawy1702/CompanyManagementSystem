using Domain.Entities;
using Domain.IRepositories;
using Persistance.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    public class ProjectRepository : GenericRepository<Project> , IProjectRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
