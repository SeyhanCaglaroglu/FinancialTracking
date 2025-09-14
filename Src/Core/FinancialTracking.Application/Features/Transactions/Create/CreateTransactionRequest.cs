using FinancialTracking.Domain.Enums;
using FinancialTracking.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Application.Features.Transactions.Create
{
    public record CreateTransactionRequest(Money Amount, string Description, TransactionType Type, int? CategoryId,string userId);
}
