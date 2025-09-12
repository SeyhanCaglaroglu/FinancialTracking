using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Application.Features.Transactions.Create
{
    public class CreateTransactionRequestValidator: AbstractValidator<CreateTransactionRequest>
    {
        public CreateTransactionRequestValidator()
        {
            RuleFor(x=>x.Amount).NotEmpty().WithMessage("Miktar Zorunludur.").Must(x=>x.Amount>0).WithMessage("Miktar 0 dan buyuk olmalidir.").Must(x=>x.Amount <= 50_000_000).WithMessage("Miktar 50 milyon’dan büyük olamaz.");

            RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Açıklama zorunludur.")
            .MaximumLength(250).WithMessage("Açıklama 250 karakterden uzun olamaz.");

            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("Geçersiz işlem tipi.");

        }
    }
}
