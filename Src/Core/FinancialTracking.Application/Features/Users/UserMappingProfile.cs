using AutoMapper;
using FinancialTracking.Application.Features.Users.Dtos;
using FinancialTracking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Application.Features.Users
{
    public class UserMappingProfile:Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User,UserDto>().ReverseMap();
            CreateMap<CreateUserDto, User>();
        }
    }
}
