using FinancialTracking.Application.Features.Users.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Application.Contracts.Auth
{
    public interface IUserService
    {
        Task<ServiceResult<UserDto>> CreateUserAsync(CreateUserDto createUserDto);
        Task<ServiceResult<UserDto>> GetUserByUserNameAsync(string userName);
        Task<ServiceResult<NoDataDto>> CreateUserRoleAsync(CreateUserRoleDto createUserRoleDto);
    }
}
