using FinancialTracking.Application.Features.Transactions.CommonDto;
using FinancialTracking.Application.Features.Transactions.Create;
using FinancialTracking.Application.Features.Transactions.Update;
using FinancialTracking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Application.Features.Transactions.Services
{
    public interface ITransactionService
    {
        Task<ServiceResult<List<TransactionDto>>> GetAllListAsync(string userId);
        Task<ServiceResult<TransactionDto>> GetByIdAsync(int id, string userId);
        Task<ServiceResult<CreateTransactionResponse>> CreateAsync(CreateTransactionRequest request);
        Task<ServiceResult> UpdateAsync(int id, UpdateTransactionRequest request);
        Task<ServiceResult> DeleteAsync(int id,string userId);
        Task<ServiceResult<List<TransactionDto>>> GetTransactionsByTypeAsync(TransactionType transactionType, string userId);
        Task<ServiceResult<List<TransactionDto>>> GetTransactionsByCategoryAsync(int categoryId, string userId);
        Task<ServiceResult<List<TransactionInCategoryDto>>> GetTransactionsInCategoryByType(TransactionType transactionType, string userId);
        Task<ServiceResult<List<TransactionInCategoryDto>>> GetTransactionsInCategoryByCategoryId(int categoryId, string userId);
    }
}
