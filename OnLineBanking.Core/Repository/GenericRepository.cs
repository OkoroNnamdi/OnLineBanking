using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OnLineBanking.Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Core.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected DbSet<T> _dbSet;
        public IQueryable<T> Table => _dbSet;
        public IQueryable<T> TableNoTracking => _dbSet.AsNoTracking();
        private readonly OnlineBankDBContext _bankDbContext;
        public GenericRepository(OnlineBankDBContext bankDbContext)
        {
            _bankDbContext = bankDbContext;
            _dbSet = bankDbContext.Set<T>();
        }
        public async  Task AddAsync(T entity)
        {
           await _dbSet.AddAsync(entity);
        }

        public async  Task DeleteAsync<T1>(T1 Value)
        {
            var entity = await _dbSet.FindAsync(Value);
            EntityEntry entityEntry = _dbSet.Remove(entity);
            entityEntry.State = EntityState.Deleted;
        }

        public virtual async  Task<IEnumerable<T>> GetAllAsync()
        {
          return   await _dbSet.ToListAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(Expression<Func<T, bool>> filter = null, bool tracked = true)
        {
            IQueryable<T> query = _dbSet;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }
        public async Task<T> GetByIdAsync(string id, T Value) => await _dbSet.FindAsync(Value);
        public async  Task UpdateAsync<T1>(T1 Value, T1 entity)
        {
            var entityUpdate = await _dbSet.FindAsync(Value);
            EntityEntry entityEntry = _dbSet.Update(entityUpdate);
            entityEntry.State = EntityState.Modified;
        }
    }
}
