using FinancialTracking.Domain.Enums;
using FinancialTracking.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Application.Features.Transactions.Update
{
    public record UpdateTransactionRequest(Money Amount, string Description, TransactionType Type, int? CategoryId, DateTime? Updated,string userId);
}
