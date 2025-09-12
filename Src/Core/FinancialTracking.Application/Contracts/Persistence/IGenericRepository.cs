using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Application.Contracts.Persistence
{
    public interface IGenericRepository<T,TId> where T : class where TId : struct
    {
        Task<bool> AnyAsync(TId id, string userId);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, string userId);
        Task<List<T>> GetAllAsync(string userId);
        Task<IQueryable<T>> WhereAsync(Expression<Func<T, bool>> predicate, string userId);
        ValueTask<T?> GetByIdAsync(TId id, string userId);
        ValueTask AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
