using FinancialTracking.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Application.Features.Goals.CommonDto
{
    public record GoalDto(string Title, Money TargetAmount, Money CurrentAmount, DateTime Deadline, DateTime Created, DateTime? Updated);
}
