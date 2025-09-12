using FinancialTracking.Application.Features.Goals;
using FinancialTracking.Domain.Entities;
using FinancialTracking.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Persistence.Features.Goals
{
    public class GoalRepository(FTDbContext context) : GenericRepository<Goal, int>(context), IGoalRepository
    {
        public Task<Goal> GetGoalByTitleAsync(string title, string userId) => Context.Goals.FirstOrDefaultAsync(x=>x.Title == title && x.UserId == userId);
    }
}
