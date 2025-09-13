using AutoMapper.Internal.Mappers;
using FinancialTracking.Application.Contracts.Auth;
using FinancialTracking.Application.Features.Users.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialTracking.Domain.Entities;
using FinancialTracking.Application;
using System.Net;
using AutoMapper;

namespace FinancialTracking.Auth.Services
{
    public class UserService(UserManager<User> _userManager, RoleManager<IdentityRole> _roleManager,IMapper _mapper) : IUserService
    {
        public async Task<ServiceResult<UserDto>> CreateUserAsync(CreateUserDto createUserDto)
        {
            var user = new User { Email = createUserDto.Email, UserName = createUserDto.UserName };

            var result = await _userManager.CreateAsync(user, createUserDto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();

                return ServiceResult<UserDto>.Fail(errors, HttpStatusCode.BadRequest);
            }

            return ServiceResult<UserDto>.Success(_mapper.Map<UserDto>(user), HttpStatusCode.Created);
        }

        public async Task<ServiceResult<NoDataDto>> CreateUserRoleAsync(CreateUserRoleDto createUserRoleDto)
        {
            // Kullanıcıyı bul
            var user = await _userManager.FindByEmailAsync(createUserRoleDto.Email);
            if (user == null)
                return ServiceResult<NoDataDto>.Fail("Email Not Found", HttpStatusCode.NotFound);

            // Rol yoksa oluştur
            if (!await _roleManager.RoleExistsAsync(createUserRoleDto.Role))
                await _roleManager.CreateAsync(new IdentityRole(createUserRoleDto.Role));

            // Kullanıcıya rol ata
            await _userManager.AddToRoleAsync(user, createUserRoleDto.Role);

            return ServiceResult<NoDataDto>.Success(HttpStatusCode.Created);
        }


        public async Task<ServiceResult<UserDto>> GetUserByUserNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return ServiceResult<UserDto>.Fail("UserName not found",HttpStatusCode.NotFound);
            }

            return ServiceResult<UserDto>.Success(_mapper.Map<UserDto>(user), HttpStatusCode.OK);
        }
    }
}
