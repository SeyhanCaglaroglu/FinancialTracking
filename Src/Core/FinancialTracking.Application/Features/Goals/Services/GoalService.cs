using AutoMapper;
using FinancialTracking.Application.Contracts.Persistence;
using FinancialTracking.Application.Features.Categories.CommonDto;
using FinancialTracking.Application.Features.Categories.Create;
using FinancialTracking.Application.Features.Categories.Services;
using FinancialTracking.Application.Features.Categories.Update;
using FinancialTracking.Application.Features.Categories;
using FinancialTracking.Application.Features.Goals.CommonDto;
using FinancialTracking.Application.Features.Goals.Create;
using FinancialTracking.Application.Features.Goals.Update;
using FinancialTracking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Application.Features.Goals.Services
{
    public class GoalService(IGoalRepository _GoalRepository, IUnitOfWork _unitOfWork, IMapper _mapper) : IGoalService
    {
        public async Task<ServiceResult<CreateGoalResponse>> CreateAsync(CreateGoalRequest request)
        {
            var anyGoal = await _GoalRepository.AnyAsync(x => x.Title == request.Title,request.userId);

            if (anyGoal)
            {
                return ServiceResult<CreateGoalResponse>.Fail("Hedef ismi zaten var.", HttpStatusCode.BadRequest);
            }

            var goal = _mapper.Map<Goal>(request);

            await _GoalRepository.AddAsync(goal);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult<CreateGoalResponse>.SuccessAsCreated(new CreateGoalResponse(goal.Id),
                $"api/categories/{goal.Id}");
        }

        public async Task<ServiceResult> DeleteAsync(int id, string userId)
        {
            var goal = await _GoalRepository.GetByIdAsync(id, userId);

            _GoalRepository.Delete(goal!);

            await _unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult<List<GoalDto>>> GetAllListAsync(string userId)
        {
            var goals = await _GoalRepository.GetAllAsync(userId);

            var GoalAsDto = _mapper.Map<List<GoalDto>>(goals);

            return ServiceResult<List<GoalDto>>.Success(GoalAsDto,HttpStatusCode.OK);
        }

        public async Task<ServiceResult<GoalDto>> GetByIdAsync(int id, string userId)
        {
            var goal = await _GoalRepository.GetByIdAsync(id, userId);

            if (goal == null)
            {
                return ServiceResult<GoalDto>.Fail("Kategori Bulunamadi.", HttpStatusCode.NotFound);
            }

            var GoalAsDto = _mapper.Map<GoalDto>(goal);

            return ServiceResult<GoalDto>.Success(GoalAsDto, HttpStatusCode.OK);
        }

        public async Task<ServiceResult<GoalDto>> GetGoalByTitleAsync(string title, string userId)
        {
            var goal = await _GoalRepository.GetGoalByTitleAsync(title, userId);

            if (goal == null)
            {
                return ServiceResult<GoalDto>.Fail("Kategori Bulunamadi.", HttpStatusCode.NotFound);
            }

            var GoalAsDto = _mapper.Map<GoalDto>(goal);

            return ServiceResult<GoalDto>.Success(GoalAsDto,HttpStatusCode.OK);
        }

        public async Task<ServiceResult> UpdateAsync(int id, UpdateGoalRequest request)
        {
            var goal = _mapper.Map<Goal>(request);
            goal.Id = id;

            _GoalRepository.Update(goal);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }
    }
}
