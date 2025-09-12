using FinancialTracking.Application.Contracts.Persistence;
using FinancialTracking.Domain.Entities;
using FinancialTracking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Application.Features.RecurringTransactions
{
    public interface IRecurringTransactionRepository:IGenericRepository<RecurringTransaction,int>
    {
        Task<List<RecurringTransaction>> GetRecurringTransactionsByTypeAsync(TransactionType Type, string userId);
        Task<List<RecurringTransaction>> GetRecurringTransactionsInCategoryByTypeAsync(TransactionType Type, string userId);
        Task<RecurringTransaction> GetRecurringTransactionsByCategoryAsync(int categoryId, string userId);
        Task<RecurringTransaction> GetRecurringTransactionsInCategoryByCategoryAsync(int categoryId, string userId);
    }
}
