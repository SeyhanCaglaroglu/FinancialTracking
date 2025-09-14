using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Application.Features.RecurringTransactions.Create
{
    public class CreateRecurringTransactionRequestValidator : AbstractValidator<CreateRecurringTransactionRequest>
    {
        public CreateRecurringTransactionRequestValidator()
        {
            RuleFor(x => x.Amount).NotEmpty().WithMessage("Miktar Zorunludur.").Must(x => x.Amount > 0).WithMessage("Miktar 0 dan buyuk olmalidir.").Must(x => x.Amount <= 5_000_000).WithMessage("Miktar 50 milyon’dan büyük olamaz.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Aciklama bos birakilamaz.");
            RuleFor(x => x.Type).IsInEnum().WithMessage("Gecersiz Tip");
            RuleFor(x => x.NextExecutionDate).GreaterThanOrEqualTo(DateTime.Today).WithMessage("Sonraki çalıştırma tarihi bugünden eski olamaz.");
            RuleFor(x => x.DayRepeatInterval).NotEmpty().WithMessage("Gün tekrar aralığı boş olamaz.").GreaterThan(0).WithMessage("Gün tekrar aralığı 0'dan büyük olmalıdır.");

        }
    }
}
