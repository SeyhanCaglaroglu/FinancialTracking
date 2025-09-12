using FinancialTracking.Application.Contracts.Persistence;
using FinancialTracking.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Persistence
{
    public class UnitOfWork(FTDbContext context) : IUnitOfWork
    {
        public Task<int> SaveChangesAsync() => context.SaveChangesAsync();
    }
}
