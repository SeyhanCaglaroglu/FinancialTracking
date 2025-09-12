using FinancialTracking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Application.Features.RefreshTokens
{
    public interface IRefreshTokenRepository
    {
        IQueryable<UserRefreshToken> Where(Expression<Func<UserRefreshToken, bool>> predicate);
        Task AddAsync(UserRefreshToken userRefreshToken);
        void Remove(UserRefreshToken userRefreshToken);
    }
}
