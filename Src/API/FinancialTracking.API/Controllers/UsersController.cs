using Asp.Versioning;
using FinancialTracking.Application.Contracts.Auth;
using FinancialTracking.Application.Features.Users.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinancialTracking.API.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsersController(IUserService _userService) : CustomBaseController
    {
        [MapToApiVersion("1")]
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(CreateUserDto createUserDto) => CreateActionResult(await _userService.CreateUserAsync(createUserDto));
    }
}
