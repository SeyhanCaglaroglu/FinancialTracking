using FinancialTracking.Application.Features.Categories.CommonDto;
using FinancialTracking.Application.Features.Categories.Create;
using FinancialTracking.Application.Features.Categories.Update;
using FinancialTracking.Application.Features.Goals.CommonDto;
using FinancialTracking.Application.Features.Goals.Create;
using FinancialTracking.Application.Features.Goals.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Application.Features.Goals.Services
{
    public interface IGoalService
    {
        Task<ServiceResult<List<GoalDto>>> GetAllListAsync(string userId);
        Task<ServiceResult<GoalDto>> GetByIdAsync(int id, string userId);
        Task<ServiceResult<CreateGoalResponse>> CreateAsync(CreateGoalRequest request, string userId);
        Task<ServiceResult> UpdateAsync(int id, UpdateGoalRequest request, string userId);
        Task<ServiceResult> DeleteAsync(int id, string userId);
        Task<ServiceResult<GoalDto>> GetGoalByTitleAsync(string title, string userId);
    }
}
