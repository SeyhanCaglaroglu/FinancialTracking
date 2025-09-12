using FinancialTracking.Application.Contracts.Persistence;
using FinancialTracking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Application.Features.Goals
{
    public interface IGoalRepository:IGenericRepository<Goal,int>
    {
        Task<Goal> GetGoalByTitleAsync(string title,string userId);
    }
}
