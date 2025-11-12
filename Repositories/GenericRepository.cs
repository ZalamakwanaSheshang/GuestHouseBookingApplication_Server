using GuestHouseBookingApplication_Server.Data;
using GuestHouseBookingApplication_Server.Models.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GuestHouseBookingApplication_Server.Repositories
{
    public class GenericRepository<T> : IRepository<T>
        where T : class
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate) =>
            await _dbSet.Where(predicate).ToListAsync();

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<T?> GetByIdAsync(object id) => await _dbSet.FindAsync(id);

        public void Remove(T entity) => _dbSet.Remove(entity);

        public void Update(T entity) => _dbSet.Update(entity);

        public void SoftDelete(T entity)
        {
            if (entity is IAuditableEntity auditable)
            {
                auditable.ActiveStatus = "Inactive";
                auditable.DeletedDate = DateTime.UtcNow;
                // userId will still be filled by DbContext automatically
            }

            _dbSet.Update(entity); // Mark entity as modified instead of deleted
        }

    }
}
