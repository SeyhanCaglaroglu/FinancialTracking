using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Application.Features.RecurringTransactions.Update
{
    public class UpdateRecurringTransactionRequestValidator:AbstractValidator<UpdateRecurringTransactionRequest>
    {
        public UpdateRecurringTransactionRequestValidator()
        {
            RuleFor(x => x.Amount).NotEmpty().WithMessage("Miktar bos birakilamaz.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Aciklama bos birakilamaz.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Tip bos birakilamaz.").IsInEnum().WithMessage("Gecersiz Tip");
            RuleFor(x => x.NextExecutionDate).GreaterThanOrEqualTo(DateTime.Today).WithMessage("Sonraki çalıştırma tarihi bugünden eski olamaz.");
            RuleFor(x => x.DayRepeatInterval).NotEmpty().WithMessage("Gün tekrar aralığı boş olamaz.").GreaterThan(0).WithMessage("Gün tekrar aralığı 0'dan büyük olmalıdır.");
        }
    }
}
