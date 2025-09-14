using FinancialTracking.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Application.Features.Goals.Update
{
    public record UpdateGoalRequest(string Title, Money TargetAmount, Money CurrentAmount, DateTime Deadline, string userId);
}
