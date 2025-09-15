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
using FinancialTracking.Application.Contracts.Caching;

namespace FinancialTracking.Application.Features.Budgets.Services
{
    public class BudgetService(IBudgetRepository _BudgetRepository, IUnitOfWork _unitOfWork, IMapper _mapper,IRedisService _redisService) : IBudgetService
    {
        private const string BudgetListCacheKey = "BudgetListCacheKey";
        public async Task<ServiceResult<CreateBudgetResponse>> CreateAsync(CreateBudgetRequest request)
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
            //Get redis
            var budgetListAsCached = await _redisService.GetAsync<List<BudgetDto>>(BudgetListCacheKey);

            if (budgetListAsCached is not null) return ServiceResult<List<BudgetDto>>.Success(budgetListAsCached);

            //get db
            var budgets = await _BudgetRepository.GetAllAsync(userId);

            var BudgetAsDto = _mapper.Map<List<BudgetDto>>(budgets);

            //add redis
            await _redisService.SetAsync(BudgetListCacheKey, BudgetAsDto, TimeSpan.FromMinutes(1));

            return ServiceResult<List<BudgetDto>>.Success(BudgetAsDto,HttpStatusCode.OK);
        }

        public async Task<ServiceResult<BudgetDto>> GetByIdAsync(int id, string userId)
        {
            var budget = await _BudgetRepository.GetByIdAsync(id, userId);

            if (budget == null)
            {
                return ServiceResult<BudgetDto>.Fail("Kategori Bulunamadi.", HttpStatusCode.NotFound);
            }

            var BudgetAsDto = _mapper.Map<BudgetDto>(budget);

            return ServiceResult<BudgetDto>.Success(BudgetAsDto,HttpStatusCode.OK);
        }

        

        public async Task<ServiceResult> UpdateAsync(int id, UpdateBudgetRequest request)
        {
            var budget = _mapper.Map<Budget>(request);
            budget.Id = id;

            _BudgetRepository.Update(budget);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }
    }
}
