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
    public class HolidayRepository : GenericRepository<Holiday> , IHolidayRepository
    {
        private readonly ApplicationDbContext _context;

        public HolidayRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
