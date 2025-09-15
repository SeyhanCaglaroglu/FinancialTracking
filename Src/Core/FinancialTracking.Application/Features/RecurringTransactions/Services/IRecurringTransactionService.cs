using FinancialTracking.Application.Features.Categories.CommonDto;
using FinancialTracking.Application.Features.Categories.Create;
using FinancialTracking.Application.Features.Categories.Update;
using FinancialTracking.Application.Features.RecurringTransactions.CommonDto;
using FinancialTracking.Application.Features.RecurringTransactions.Create;
using FinancialTracking.Application.Features.RecurringTransactions.Update;
using FinancialTracking.Domain.Entities;
using FinancialTracking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Application.Features.RecurringTransactions.Services
{
    public interface IRecurringTransactionService
    {
        Task<ServiceResult<List<RecurringTransactionDto>>> GetAllListAsync(string userId);
        Task<ServiceResult<RecurringTransactionDto>> GetByIdAsync(int id, string userId);
        Task<ServiceResult<CreateRecurringTransactionResponse>> CreateAsync(CreateRecurringTransactionRequest request);
        Task<ServiceResult> UpdateAsync(int id, UpdateRecurringTransactionRequest request);
        Task<ServiceResult> DeleteAsync(int id, string userId);
        Task<ServiceResult<List<RecurringTransactionDto>>> GetRecurringTransactionsByTypeAsync(TransactionType Type, string userId);
        Task<ServiceResult<List<RecurringTransactionDto>>> GetRecurringTransactionsByCategoryAsync(int categoryId, string userId);

        Task<ServiceResult<List<RecurringTransactionInCategoryDto>>> GetRecurringTransactionsInCategoryByTypeAsync(TransactionType Type, string userId);
        Task<ServiceResult<List<RecurringTransactionInCategoryDto>>> GetRecurringTransactionsInCategoryByCategoryAsync(int categoryId, string userId);
    }
}
