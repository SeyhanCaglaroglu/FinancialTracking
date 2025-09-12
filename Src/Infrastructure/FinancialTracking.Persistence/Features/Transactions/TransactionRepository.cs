using FinancialTracking.Application.Features.Transactions;
using FinancialTracking.Domain.Entities;
using FinancialTracking.Domain.Enums;
using FinancialTracking.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Persistence.Features.Transactions
{
    public class TransactionRepository(FTDbContext context) : GenericRepository<Transaction, int>(context), ITransactionRepository
    {
        public Task<List<Transaction>> GetTransactionsByCategory(int categoryId, string userId) => Context.Transactions.Where(t => t.CategoryId == categoryId && t.UserId == userId).ToListAsync();

        public Task<List<Transaction>> GetTransactionsByType(TransactionType transactionType, string userId) => Context.Transactions.Where(t => t.Type == transactionType && t.UserId == userId).ToListAsync();

        public Task<List<Transaction>> GetTransactionsInCategoryByCategoryId(int categoryId, string userId) => Context.Transactions.Include(x=>x.Category).Where(t => t.CategoryId == categoryId && t.UserId == userId).ToListAsync();

        public Task<List<Transaction>> GetTransactionsInCategoryByType(TransactionType transactionType, string userId) => Context.Transactions.Include(x=>x.Category).Where(t => t.Type == transactionType && t.UserId == userId).ToListAsync();
    }
}
