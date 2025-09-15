using AutoMapper;
using FinancialTracking.Application.Contracts.Persistence;
using FinancialTracking.Application.Features.Transactions.CommonDto;
using FinancialTracking.Application.Features.Transactions.Create;
using FinancialTracking.Application.Features.Transactions.Update;
using FinancialTracking.Domain.Entities;
using FinancialTracking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Application.Features.Transactions.Services
{
    public class TransactionService(ITransactionRepository _transactionRepository,IUnitOfWork _unitOfWork,IMapper _mapper) : ITransactionService
    {
        public async Task<ServiceResult<CreateTransactionResponse>> CreateAsync(CreateTransactionRequest request)
        {
            var transaction = _mapper.Map<Transaction>(request);

            await _transactionRepository.AddAsync(transaction);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult<CreateTransactionResponse>.SuccessAsCreated(new CreateTransactionResponse(transaction.Id), $"api/transactions/{transaction.Id}");
        }

        public async Task<ServiceResult> DeleteAsync(int id, string userId)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id, userId);

            _transactionRepository.Delete(transaction!);

            await _unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult<List<TransactionDto>>> GetAllListAsync(string userId)
        {
            var transactions = await _transactionRepository.GetAllAsync(userId);

            var transactionAsDto = transactions != null
                ? _mapper.Map<List<TransactionDto>>(transactions)
                : new List<TransactionDto>(); // boş liste

            return ServiceResult<List<TransactionDto>>.Success(transactionAsDto, HttpStatusCode.OK);
        }


        public async Task<ServiceResult<TransactionDto>> GetByIdAsync(int id, string userId)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id, userId);

            if (transaction == null)
            {
                return ServiceResult<TransactionDto>.Fail("Transaction Not Found !", HttpStatusCode.NotFound);
            }

            var tansactionAsDto = _mapper.Map<TransactionDto>(transaction);

            return ServiceResult<TransactionDto>.Success(tansactionAsDto, HttpStatusCode.OK);
        }

        public async Task<ServiceResult<List<TransactionDto>>> GetTransactionsByCategoryAsync(int categoryId, string userId)
        {
            var transactions = await _transactionRepository.GetTransactionsByCategory(categoryId,userId);

            var transactionAsDto = _mapper.Map<List<TransactionDto>>(transactions);

            return ServiceResult<List<TransactionDto>>.Success(transactionAsDto, HttpStatusCode.OK);
        }

        public async Task<ServiceResult<List<TransactionDto>>> GetTransactionsByTypeAsync(TransactionType transactionType, string userId)
        {
            var transactions = await _transactionRepository.GetTransactionsByType(transactionType, userId);

            var transactionAsDto = _mapper.Map<List<TransactionDto>>(transactions);

            return ServiceResult<List<TransactionDto>>.Success(transactionAsDto, HttpStatusCode.OK);
        }

        public async Task<ServiceResult<List<TransactionInCategoryDto>>> GetTransactionsInCategoryByCategoryId(int categoryId, string userId)
        {
            var transactions = await _transactionRepository.GetTransactionsInCategoryByCategoryId(categoryId, userId);

            var transactionAsDto = _mapper.Map<List<TransactionInCategoryDto>>(transactions);

            return ServiceResult<List<TransactionInCategoryDto>>.Success(transactionAsDto, HttpStatusCode.OK);
        }

        public async Task<ServiceResult<List<TransactionInCategoryDto>>> GetTransactionsInCategoryByType(TransactionType transactionType, string userId)
        {
            var transactions = await _transactionRepository.GetTransactionsInCategoryByType(transactionType, userId);

            var transactionAsDto = _mapper.Map<List<TransactionInCategoryDto>>(transactions);

            return ServiceResult<List<TransactionInCategoryDto>>.Success(transactionAsDto, HttpStatusCode.OK);
        }

        public async Task<ServiceResult> UpdateAsync(int id, UpdateTransactionRequest request)
        {
            var transaction = _mapper.Map<Transaction>(request);
            transaction.Id = id;

            _transactionRepository.Update(transaction);
            await _unitOfWork.SaveChangesAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent);
        }
    }
}
