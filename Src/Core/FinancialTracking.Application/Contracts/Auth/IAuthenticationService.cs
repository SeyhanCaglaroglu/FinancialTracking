using FinancialTracking.Application.Features.Users.Client;
using FinancialTracking.Application.Features.Users.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Application.Contracts.Auth
{
    public interface IAuthenticationService
    {
        Task<ServiceResult<TokenDto>> CreateToken(LoginDto loginDto);
        Task<ServiceResult<TokenDto>> CreateTokenByRefreshToken(string refreshToken);
        Task<ServiceResult<NoDataDto>> RevokeRefreshToken(string refreshToken);
        ServiceResult<ClientTokenDto> CreateClientToken(ClientLoginDto clientLoginDto);
    }
}
