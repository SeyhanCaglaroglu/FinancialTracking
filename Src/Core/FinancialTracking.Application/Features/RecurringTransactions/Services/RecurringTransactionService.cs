using AutoMapper;
using FinancialTracking.Application.Contracts.Persistence;
using FinancialTracking.Application.Features.Categories.CommonDto;
using FinancialTracking.Application.Features.Categories.Create;
using FinancialTracking.Application.Features.Categories.Services;
using FinancialTracking.Application.Features.Categories.Update;
using FinancialTracking.Application.Features.Categories;
using FinancialTracking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FinancialTracking.Application.Features.RecurringTransactions.Create;
using FinancialTracking.Application.Features.RecurringTransactions.CommonDto;
using FinancialTracking.Application.Features.RecurringTransactions.Update;
using FinancialTracking.Domain.Enums;

namespace FinancialTracking.Application.Features.RecurringTransactions.Services
{
    public class RecurringTransactionService(IRecurringTransactionRepository _recurringTransactionRepository, IUnitOfWork _unitOfWork, IMapper _mapper) : IRecurringTransactionService
    {
        public async Task<ServiceResult<CreateRecurringTransactionResponse>> CreateAsync(CreateRecurringTransactionRequest request)
        {
            var recurringTransaction = _mapper.Map<RecurringTransaction>(request);

            await _recurringTransactionRepository.AddAsync(recurringTransaction);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult<CreateRecurringTransactionResponse>.SuccessAsCreated(new CreateRecurringTransactionResponse(recurringTransaction.Id),
                $"api/categories/{recurringTransaction.Id}");
        }

        public async Task<ServiceResult> DeleteAsync(int id, string userId)
        {
            var recurringTransaction = await _recurringTransactionRepository.GetByIdAsync(id, userId);

            _recurringTransactionRepository.Delete(recurringTransaction!);

            await _unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult<List<RecurringTransactionDto>>> GetAllListAsync(string userId)
        {
            var recurringTransactions = await _recurringTransactionRepository.GetAllAsync(userId);

            var RecurringTransactionAsDto = _mapper.Map<List<RecurringTransactionDto>>(recurringTransactions);

            return ServiceResult<List<RecurringTransactionDto>>.Success(RecurringTransactionAsDto,HttpStatusCode.OK);
        }

        public async Task<ServiceResult<RecurringTransactionDto>> GetByIdAsync(int id, string userId)
        {
            var recurringTransaction = await _recurringTransactionRepository.GetByIdAsync(id, userId);

            if (recurringTransaction == null)
            {
                return ServiceResult<RecurringTransactionDto>.Fail("Kategori Bulunamadi.", HttpStatusCode.NotFound);
            }

            var RecurringTransactionAsDto = _mapper.Map<RecurringTransactionDto>(recurringTransaction);

            return ServiceResult<RecurringTransactionDto>.Success(RecurringTransactionAsDto,HttpStatusCode.OK);
        }

        public async Task<ServiceResult<List<RecurringTransactionDto>>> GetRecurringTransactionsByCategoryAsync(int categoryId, string userId)
        {
            var recurringTransactions = await _recurringTransactionRepository.GetRecurringTransactionsByCategoryAsync(categoryId,userId);

            var RecurringTransactionAsDto = _mapper.Map<List<RecurringTransactionDto>>(recurringTransactions);

            return ServiceResult<List<RecurringTransactionDto>>.Success(RecurringTransactionAsDto, HttpStatusCode.OK);
        }

        public async Task<ServiceResult<List<RecurringTransactionDto>>> GetRecurringTransactionsByTypeAsync(TransactionType Type, string userId)
        {
            var recurringTransactions = await _recurringTransactionRepository.GetRecurringTransactionsByTypeAsync(Type, userId);

            var RecurringTransactionAsDto = _mapper.Map<List<RecurringTransactionDto>>(recurringTransactions);

            return ServiceResult<List<RecurringTransactionDto>>.Success(RecurringTransactionAsDto, HttpStatusCode.OK);
        }

        public async Task<ServiceResult<List<RecurringTransactionInCategoryDto>>> GetRecurringTransactionsInCategoryByCategoryAsync(int categoryId, string userId)
        {
            var recurringTransactions = await _recurringTransactionRepository.GetRecurringTransactionsInCategoryByCategoryAsync(categoryId, userId);

            var RecurringTransactionAsDto = _mapper.Map<List<RecurringTransactionInCategoryDto>>(recurringTransactions);

            return ServiceResult<List<RecurringTransactionInCategoryDto>>.Success(RecurringTransactionAsDto, HttpStatusCode.OK);
        }

        public async Task<ServiceResult<List<RecurringTransactionInCategoryDto>>> GetRecurringTransactionsInCategoryByTypeAsync(TransactionType Type, string userId)
        {
            var recurringTransactions = await _recurringTransactionRepository.GetRecurringTransactionsInCategoryByTypeAsync(Type, userId);

            var RecurringTransactionAsDto = _mapper.Map<List<RecurringTransactionInCategoryDto>>(recurringTransactions);

            return ServiceResult<List<RecurringTransactionInCategoryDto>>.Success(RecurringTransactionAsDto, HttpStatusCode.OK);
        }

        public async Task<ServiceResult> UpdateAsync(int id, UpdateRecurringTransactionRequest request)
        {
            var recurringTransaction = _mapper.Map<RecurringTransaction>(request);
            recurringTransaction.Id = id;

            _recurringTransactionRepository.Update(recurringTransaction);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }
    }
}
