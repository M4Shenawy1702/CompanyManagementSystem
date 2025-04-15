using Domain.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistance.DbContext;
using System.Linq.Expressions;

namespace Persistance.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        public GenericRepository(ApplicationDbContext context) => _context = context;
        public async Task<T> GetByIdAsync(int Id)
        {
            return await _context.Set<T>().FindAsync(Id);
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> InsertAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public T Delete(T entity)
        {

            _context.Set<T>().Remove(entity);
            return entity;
        }
        public async Task<T> FindAsync(Expression<Func<T, bool>> craiteria, string[] includes = null)
        {
            IQueryable<T> Query = _context.Set<T>();
            if (includes != null)
                foreach (var include in includes)
                    Query = Query.Include(include);

            return await Query.FirstOrDefaultAsync(craiteria);
        }

        public async Task<IEnumerable<T>> FindAllWithcraiteriaAsync(Expression<Func<T, bool>> craiteria, string[] includes = null)
        {
            IQueryable<T> Query = _context.Set<T>();
            if (includes != null)
                foreach (var include in includes)
                    Query = Query.Include(include);

            return await Query.Where(craiteria).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAllWithIncludesAsync(string[] includes)
        {
            IQueryable<T> Query = _context.Set<T>();
            if (includes != null)
                foreach (var include in includes)
                    Query = Query.Include(include);

            return await Query.ToListAsync();
        }


        public T Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return entity;
        }

        internal async Task<byte[]> ValidateAndProcessProfileImageAsync(IFormFile profileImage)
        {
            // Ensure file is not null
            if (profileImage == null)
                throw new ArgumentException("Profile image is required.");

            // Validate file extension
            var allowedExtensions = new List<string> { ".jpg", ".png" };
            var fileExtension = Path.GetExtension(profileImage.FileName).ToLower();
            if (!allowedExtensions.Contains(fileExtension))
                throw new ArgumentException("Only .jpg and .png images are allowed.");

            // Validate file size (10 MB maximum)
            const long maxAllowedSize = 10 * 1024 * 1024; // 10 MB
            if (profileImage.Length > maxAllowedSize)
                throw new ArgumentException("Maximum allowed file size is 10 MB.");

            // Convert file to byte array
            using var memoryStream = new MemoryStream();
            await profileImage.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }
}
