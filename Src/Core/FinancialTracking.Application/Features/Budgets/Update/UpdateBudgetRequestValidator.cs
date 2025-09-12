using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Application.Features.Budgets.Update
{
    public class UpdateBudgetRequestValidator:AbstractValidator<UpdateBudgetRequest>
    {
        public UpdateBudgetRequestValidator()
        {
            RuleFor(x => x.TotalAmount).NotEmpty().WithMessage("Bu alan Zorunludur.").Must(x => x.Amount > 0).WithMessage("Miktar 0 dan buyuk olmalidir.").Must(x => x.Amount <= 50_000_000).WithMessage("Miktar 50 milyon’dan büyük olamaz.");
        }
    }
}
