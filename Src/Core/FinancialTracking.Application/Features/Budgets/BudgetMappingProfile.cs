using AutoMapper;
using FinancialTracking.Application.Features.Budgets.CommonDto;
using FinancialTracking.Application.Features.Budgets.Create;
using FinancialTracking.Application.Features.Budgets.Update;
using FinancialTracking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Application.Features.Budgets
{
    public class BudgetMappingProfile : Profile
    {
        public BudgetMappingProfile()
        {
            CreateMap<Budget,CreateBudgetRequest>().ReverseMap();
            CreateMap<Budget,UpdateBudgetRequest>().ReverseMap();
            CreateMap<Budget,BudgetDto>().ReverseMap();
        }
    }
}
