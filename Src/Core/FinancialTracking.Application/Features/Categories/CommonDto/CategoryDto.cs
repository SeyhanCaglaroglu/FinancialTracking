using FinancialTracking.Application.Features.RecurringTransactions.CommonDto;
using FinancialTracking.Application.Features.Transactions.CommonDto;
using FinancialTracking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Application.Features.Categories.CommonDto
{
    public record CategoryDto(int Id,string Name, DateTime Created, DateTime Updated, ICollection<TransactionDto> Transactions, ICollection<RecurringTransactionDto> RecurringTransactions);
}
