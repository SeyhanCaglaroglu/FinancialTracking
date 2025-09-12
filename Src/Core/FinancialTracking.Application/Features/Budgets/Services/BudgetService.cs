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
using FinancialTracking.Application.Features.Budgets.Update;
using FinancialTracking.Application.Features.Budgets.CommonDto;
using FinancialTracking.Application.Features.Budgets.Create;

namespace FinancialTracking.Application.Features.Budgets.Services
{
    public class BudgetService(IBudgetRepository _BudgetRepository, IUnitOfWork _unitOfWork, IMapper _mapper) : IBudgetService
    {
        public async Task<ServiceResult<CreateBudgetResponse>> CreateAsync(CreateBudgetRequest request, string userId)
        {
            var budget = _mapper.Map<Budget>(request);

            await _BudgetRepository.AddAsync(budget);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult<CreateBudgetResponse>.SuccessAsCreated(new CreateBudgetResponse(budget.Id),
                $"api/categories/{budget.Id}");
        }

        public async Task<ServiceResult> DeleteAsync(int id, string userId)
        {
            var budget = await _BudgetRepository.GetByIdAsync(id, userId);

            _BudgetRepository.Delete(budget!);

            await _unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult<List<BudgetDto>>> GetAllListAsync(string userId)
        {
            var budgets = await _BudgetRepository.GetAllAsync(userId);

            var BudgetAsDto = _mapper.Map<List<BudgetDto>>(budgets);

            return ServiceResult<List<BudgetDto>>.Success(BudgetAsDto);
        }

        public async Task<ServiceResult<BudgetDto>> GetByIdAsync(int id, string userId)
        {
            var budget = await _BudgetRepository.GetByIdAsync(id, userId);

            if (budget == null)
            {
                return ServiceResult<BudgetDto>.Fail("Kategori Bulunamadi.", HttpStatusCode.NotFound);
            }

            var BudgetAsDto = _mapper.Map<BudgetDto>(budget);

            return ServiceResult<BudgetDto>.Success(BudgetAsDto);
        }

        

        public async Task<ServiceResult> UpdateAsync(int id, UpdateBudgetRequest request, string userId)
        {
            var budget = _mapper.Map<Budget>(request);
            budget.Id = id;
            budget.UserId = userId;

            _BudgetRepository.Update(budget);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }
    }
}
