using Asp.Versioning;
using FinancialTracking.Application;
using FinancialTracking.Application.Contracts.Auth;
using FinancialTracking.Application.Features.Users.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinancialTracking.API.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Tags("Users")]
    public class UsersController(IUserService _userService) : CustomBaseController
    {
        [AllowAnonymous]
        [MapToApiVersion("1")]
        [HttpPost("CreateUser", Name = "CreateUser")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResult<UserDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ServiceResult<UserDto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResult<UserDto>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateUser(CreateUserDto createUserDto) => CreateActionResult(await _userService.CreateUserAsync(createUserDto));

        [MapToApiVersion("1")]
        [HttpPost("CreateUserRole",Name = "CreateUserRole")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResult<NoDataDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ServiceResult<NoDataDto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResult<NoDataDto>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ServiceResult<NoDataDto>), StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<IActionResult> CreateUserRole(CreateUserRoleDto createUserRoleDto) => CreateActionResult(await _userService.CreateUserRoleAsync(createUserRoleDto));

        [MapToApiVersion("1")]
        [HttpGet("GetUserByUserName/{userName}",Name = "GetUserByUserName")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResult<UserDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResult<UserDto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResult<UserDto>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ServiceResult<UserDto>), StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<IActionResult> GetUserByUserName(string userName) => CreateActionResult(await _userService.GetUserByUserNameAsync(userName));

    }


}
