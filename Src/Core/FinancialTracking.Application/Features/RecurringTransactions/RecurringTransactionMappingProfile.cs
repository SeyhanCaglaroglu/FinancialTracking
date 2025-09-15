using AutoMapper;
using FinancialTracking.Application.Features.RecurringTransactions.CommonDto;
using FinancialTracking.Application.Features.RecurringTransactions.Create;
using FinancialTracking.Application.Features.RecurringTransactions.Update;
using FinancialTracking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Application.Features.RecurringTransactions
{
    public class RecurringTransactionMappingProfile:Profile
    {
        public RecurringTransactionMappingProfile()
        {
            CreateMap<RecurringTransaction, RecurringTransactionDto>().ReverseMap();
            CreateMap<RecurringTransaction, CreateRecurringTransactionRequest>().ReverseMap();
            CreateMap<RecurringTransaction, UpdateRecurringTransactionRequest>().ReverseMap();
            CreateMap<RecurringTransaction, RecurringTransactionInCategoryDto>().ReverseMap();
        }
    }
}
