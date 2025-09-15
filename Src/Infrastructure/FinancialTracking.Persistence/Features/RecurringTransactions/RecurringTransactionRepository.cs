using FinancialTracking.Application.Features.RecurringTransactions;
using FinancialTracking.Domain.Entities;
using FinancialTracking.Domain.Enums;
using FinancialTracking.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Persistence.Features.RecurringTransactions
{
    public class RecurringTransactionRepository(FTDbContext context) : GenericRepository<RecurringTransaction, int>(context), IRecurringTransactionRepository
    {
        public Task<List<RecurringTransaction>> GetRecurringTransactionsByCategoryAsync(int categoryId, string userId) => Context.RecurringTransactions.Where(x=>x.CategoryId == categoryId && x.UserId == userId).ToListAsync();

        public Task<List<RecurringTransaction>> GetRecurringTransactionsInCategoryByCategoryAsync(int categoryId, string userId) => Context.RecurringTransactions.Include(x=>x.Category).Where(x => x.CategoryId == categoryId && x.UserId == userId).ToListAsync();

        public Task<List<RecurringTransaction>> GetRecurringTransactionsInCategoryByTypeAsync(TransactionType Type, string userId) => Context.RecurringTransactions.Include(x=>x.Category).Where(x=>x.Type == Type && x.UserId == userId).ToListAsync();

        public Task<List<RecurringTransaction>> GetRecurringTransactionsByTypeAsync(TransactionType Type, string userId)
        => Context.RecurringTransactions.Where(x => x.Type == Type && x.UserId == userId).ToListAsync();
    }
}
