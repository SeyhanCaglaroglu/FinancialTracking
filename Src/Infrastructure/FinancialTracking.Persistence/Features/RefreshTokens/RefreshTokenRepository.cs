using FinancialTracking.Application.Features.RefreshTokens;
using FinancialTracking.Domain.Entities;
using FinancialTracking.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Persistence.Features.RefreshTokens
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly FTDbContext _context;
        private readonly DbSet<UserRefreshToken> _dbSet;

        public RefreshTokenRepository(FTDbContext context)
        {
            _context = context; 
            _dbSet = _context.UserRefreshTokens;
        }
        public async Task AddAsync(UserRefreshToken userRefreshToken)
        {
            await _dbSet.AddAsync(userRefreshToken);
        }

        public void Remove(UserRefreshToken userRefreshToken)
        {
            _dbSet.Remove(userRefreshToken);
        }

        public IQueryable<UserRefreshToken> Where(Expression<Func<UserRefreshToken, bool>> predicate)
        {
           return _dbSet.Where(predicate);
        }
    }
}
