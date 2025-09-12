using FinancialTracking.Application.Contracts.Persistence;
using FinancialTracking.Domain.Entities.Common;
using FinancialTracking.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Persistence
{
    public class GenericRepository<T, TId>(FTDbContext context) : IGenericRepository<T, TId> where T : BaseEntity<TId> where TId : struct
    {
        protected FTDbContext Context = context;

        private readonly DbSet<T> _dbSet = context.Set<T>();
        public async ValueTask AddAsync(T entity) => await _dbSet.AddAsync(entity);

        public Task<bool> AnyAsync(TId id, string userId) => _dbSet.AnyAsync(x => x.Id.Equals(id) && x.UserId ==userId);

        public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, string userId) => _dbSet.Where(x => x.UserId == userId).AnyAsync(predicate);

        public void Delete(T entity) => _dbSet.Remove(entity);

        public Task<List<T>> GetAllAsync(string userId) => _dbSet.Where(x => x.UserId == userId).ToListAsync();

        public async ValueTask<T?> GetByIdAsync(TId id, string userId) => await _dbSet.FirstOrDefaultAsync(x => x.Id.Equals(id) && x.UserId == userId);

        public void Update(T entity) => _dbSet.Update(entity);

        public async Task<IQueryable<T>> WhereAsync(Expression<Func<T, bool>> predicate, string userId)
        {
            var query = _dbSet.Where(x => x.UserId == userId).Where(predicate).AsNoTracking();
            return await Task.FromResult(query);
        }
    }
}
