using FinancialTracking.Domain.Enums;
using FinancialTracking.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Application.Features.RecurringTransactions.Update
{
    public record UpdateRecurringTransactionRequest(Money Amount, string Description, TransactionType Type, DateTime NextExecutionDate, int DayRepeatInterval, int? CategoryId);
}
