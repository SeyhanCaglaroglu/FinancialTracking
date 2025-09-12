using FinancialTracking.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Application.Features.Budgets.CommonDto
{
    public record BudgetDto(Money TotalAmount, DateTime Created, DateTime? Updated);
}
