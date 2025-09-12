using FinancialTracking.Application.Contracts.Persistence;
using FinancialTracking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Application.Features.Budgets
{
    public interface IBudgetRepository:IGenericRepository<Budget,int>
    {

    }
}
