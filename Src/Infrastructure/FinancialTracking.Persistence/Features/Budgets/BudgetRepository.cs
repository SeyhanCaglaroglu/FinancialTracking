using FinancialTracking.Application.Features.Budgets;
using FinancialTracking.Domain.Entities;
using FinancialTracking.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Persistence.Features.Budgets
{
    public class BudgetRepository(FTDbContext context):GenericRepository<Budget,int>(context),IBudgetRepository
    {
    }
}
