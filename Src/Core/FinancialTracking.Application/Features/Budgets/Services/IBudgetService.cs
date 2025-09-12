using FinancialTracking.Application.Features.Budgets.CommonDto;
using FinancialTracking.Application.Features.Budgets.Create;
using FinancialTracking.Application.Features.Budgets.Update;
using FinancialTracking.Application.Features.Categories.CommonDto;
using FinancialTracking.Application.Features.Categories.Create;
using FinancialTracking.Application.Features.Categories.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Application.Features.Budgets.Services
{
    public interface IBudgetService
    {
        Task<ServiceResult<List<BudgetDto>>> GetAllListAsync(string userId);
        Task<ServiceResult<BudgetDto>> GetByIdAsync(int id, string userId);
        Task<ServiceResult<CreateBudgetResponse>> CreateAsync(CreateBudgetRequest request, string userId);
        Task<ServiceResult> UpdateAsync(int id, UpdateBudgetRequest request, string userId);
        Task<ServiceResult> DeleteAsync(int id, string userId);
    }
}
