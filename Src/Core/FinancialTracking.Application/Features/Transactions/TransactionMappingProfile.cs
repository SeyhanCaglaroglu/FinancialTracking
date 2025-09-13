using AutoMapper;
using FinancialTracking.Application.Features.Transactions.CommonDto;
using FinancialTracking.Application.Features.Transactions.Create;
using FinancialTracking.Application.Features.Transactions.Update;
using FinancialTracking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Application.Features.Transactions
{
    public class TransactionMappingProfile: Profile
    {
        public TransactionMappingProfile()
        {
            CreateMap<Transaction, TransactionDto>().ReverseMap();

            CreateMap<CreateTransactionRequest, Transaction>().ReverseMap();

            CreateMap<UpdateTransactionRequest, Transaction>().ReverseMap();
        }
    }
}
