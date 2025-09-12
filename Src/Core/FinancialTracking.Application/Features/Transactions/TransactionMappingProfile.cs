using AutoMapper;
using FinancialTracking.Application.Features.Transactions.CommonDto;
using FinancialTracking.Application.Features.Transactions.Create;
using FinancialTracking.Application.Features.Transactions.Update;
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
            CreateMap<Domain.Entities.Transaction, TransactionDto>().ReverseMap();

            CreateMap<CreateTransactionRequest, Domain.Entities.Transaction>().ReverseMap();

            CreateMap<UpdateTransactionRequest, Domain.Entities.Transaction>().ReverseMap();
        }
    }
}
