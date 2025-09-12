using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Application.Contracts.Persistence
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
    }
}
