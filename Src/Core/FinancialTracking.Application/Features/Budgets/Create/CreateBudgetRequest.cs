using FinancialTracking.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Application.Features.Budgets.Create
{
    public record CreateBudgetRequest(Money TotalAmount,string userId);
}
