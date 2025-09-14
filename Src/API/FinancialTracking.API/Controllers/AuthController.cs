using Asp.Versioning;
using FinancialTracking.Application;
using FinancialTracking.Application.Contracts.Auth;
using FinancialTracking.Application.Features.RefreshTokens;
using FinancialTracking.Application.Features.Users.Client;
using FinancialTracking.Application.Features.Users.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinancialTracking.API.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Tags("Auth")]
    public class AuthController(IAuthenticationService _authenticationService) : CustomBaseController
    {
        [MapToApiVersion("1")]
        [HttpPost("CreateToken",Name = "CreateToken")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResult<TokenDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ServiceResult<TokenDto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResult<TokenDto>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateToken(LoginDto loginDto) => CreateActionResult(await _authenticationService.CreateToken(loginDto));

        [MapToApiVersion("1")]
        [HttpPost("CreateTokenByRefreshToken",Name = "CreateTokenByRefreshToken")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResult<TokenDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ServiceResult<TokenDto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResult<TokenDto>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ServiceResult<TokenDto>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateTokenByRefreshToken(RefreshTokenDto refreshTokenDto) => CreateActionResult(await _authenticationService.CreateTokenByRefreshToken(refreshTokenDto.Token));

        [MapToApiVersion("1")]
        [HttpPost("CreateClientToken",Name = "CreateClientToken")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResult<ClientTokenDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ServiceResult<ClientTokenDto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResult<ClientTokenDto>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ServiceResult<ClientTokenDto>), StatusCodes.Status500InternalServerError)]
        public IActionResult CreateClientToken(ClientLoginDto clientLoginDto) => CreateActionResult(_authenticationService.CreateClientToken(clientLoginDto));

        [MapToApiVersion("1")]
        [HttpDelete("RevokeRefreshToken")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResult<NoDataDto>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ServiceResult<NoDataDto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResult<NoDataDto>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ServiceResult<NoDataDto>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RevokeRefreshToken(RefreshTokenDto refreshTokenDto) => CreateActionResult(await  _authenticationService.RevokeRefreshToken(refreshTokenDto.Token));
    }
}
