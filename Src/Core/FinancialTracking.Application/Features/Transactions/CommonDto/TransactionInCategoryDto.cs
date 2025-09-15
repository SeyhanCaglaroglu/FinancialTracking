using FinancialTracking.Domain.Entities;
using FinancialTracking.Domain.Enums;
using FinancialTracking.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Application.Features.Transactions.CommonDto
{
    public record TransactionInCategoryDto(int Id, Money Amount, string Description, TransactionType Type, DateTime Created, DateTime Updated, int CategoryId,Category Category);
}
