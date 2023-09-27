using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Core.IRepository
{
    public  interface IGenericRepositry<T>where T: class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null);
        Task<T> GetByIdAsync(Expression<Func<T, bool>> filter = null, bool tracked = true);
        Task AddAsync(T entity);
        Task UpdateAsync<T>(T Value, T entity);
        Task DeleteAsync<T>(T Value);
    }
}
