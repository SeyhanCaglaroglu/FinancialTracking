using AutoMapper;
using FinancialTracking.Application.Features.Goals.CommonDto;
using FinancialTracking.Application.Features.Goals.Create;
using FinancialTracking.Application.Features.Goals.Update;
using FinancialTracking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Application.Features.Goals
{
    public class GoalMappingProfile:Profile
    {
        public GoalMappingProfile()
        {
            CreateMap<Goal,GoalDto>().ReverseMap();
            CreateMap<Goal,CreateGoalRequest>().ReverseMap();
            CreateMap<Goal,UpdateGoalRequest>().ReverseMap();
        }
    }
}
