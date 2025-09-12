using FinancialTracking.Application.Contracts.Persistence;
using FinancialTracking.Domain.Entities;
using FinancialTracking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Application.Features.Transactions
{
    public interface ITransactionRepository : IGenericRepository<Transaction,int>
    {
        Task<List<Transaction>> GetTransactionsByType(TransactionType transactionType, string userId);
        Task<List<Transaction>> GetTransactionsByCategory(int categoryId,string userId);
        Task<List<Transaction>> GetTransactionsInCategoryByType(TransactionType transactionType, string userId);
        Task<List<Transaction>> GetTransactionsInCategoryByCategoryId(int categoryId, string userId);
    }
}
