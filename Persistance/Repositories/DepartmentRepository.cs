using Domain.Dtos;
using Domain.Entities;
using Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using Persistance.DbContext;
using System.Numerics;

namespace Persistance.Repositories
{

    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;

        public DepartmentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

    }
}
