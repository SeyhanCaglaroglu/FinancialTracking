using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Application.Features.Goals.Update
{
    public class UpdateGoalRequestValidator: AbstractValidator<UpdateGoalRequest>
    {
        public UpdateGoalRequestValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Baslik bos birakilamaz.");
            RuleFor(x => x.TargetAmount).NotEmpty().WithMessage("Hedef Miktar girilmelidir.");
            RuleFor(x => x.CurrentAmount).NotEmpty().WithMessage("Suanki Miktar girilmelidir.");
            RuleFor(x => x.Deadline).NotEmpty().WithMessage("Hedefinizi hangi tarihe kadar gerceklestirmek istiyorsunuz?");
        }
    }
}
