using System.Linq.Expressions;

namespace Domain.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        public Task<T> GetByIdAsync(int Id);
        public Task<T> InsertAsync(T entity);
        public T Delete(T entity);
        public T Update(T entity);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> FindAsync(Expression<Func<T, bool>> craiteria, string[] includes = null);
        public Task<IEnumerable<T>> FindAllWithcraiteriaAsync(Expression<Func<T, bool>> craiteria, string[] includes = null);
        public Task<IEnumerable<T>> FindAllWithIncludesAsync(string[] includes);
    }
}
