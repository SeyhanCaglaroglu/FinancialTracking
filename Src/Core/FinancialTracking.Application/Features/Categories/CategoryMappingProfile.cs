using AutoMapper;
using FinancialTracking.Application.Features.Categories.CommonDto;
using FinancialTracking.Application.Features.Categories.Create;
using FinancialTracking.Application.Features.Categories.Update;
using FinancialTracking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Application.Features.Categories
{
    public class CategoryMappingProfile: Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<Category,CategoryDto>().ReverseMap();
            CreateMap<Category,CreateCategoryRequest>().ReverseMap();
            CreateMap<Category,UpdateCategoryRequest>().ReverseMap();
        }
    }
}
