using FinancialTracking.Application;
using FinancialTracking.Application.Contracts.Auth;
using FinancialTracking.Application.Contracts.Persistence;
using FinancialTracking.Application.Features.RefreshTokens;
using FinancialTracking.Application.Features.Users.Client;
using FinancialTracking.Application.Features.Users.Dtos;
using FinancialTracking.Domain.Configuration;
using FinancialTracking.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Auth.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly List<Client> _clients;
        private readonly ITokenService _tokenService;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public AuthenticationService(IOptions<List<Client>> optionsClient, ITokenService tokenService, UserManager<User> userManager, IUnitOfWork unitOfWork, IRefreshTokenRepository refreshTokenRepository)
        {
            _clients = optionsClient.Value;
            _tokenService = tokenService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public ServiceResult<ClientTokenDto> CreateClientToken(ClientLoginDto clientLoginDto)
        {
            var client = _clients.SingleOrDefault(x => x.Id == clientLoginDto.ClientId && x.Secret == clientLoginDto.ClientSecret);

            if (client == null)
            {
                return ServiceResult<ClientTokenDto>.Fail("ClientId or ClientSecret not found",HttpStatusCode.NotFound);
            }

            var token = _tokenService.CreateClientToken(client);

            return ServiceResult<ClientTokenDto>.Success(token, HttpStatusCode.Created);
        }

        public async Task<ServiceResult<TokenDto>> CreateToken(LoginDto loginDto)
        {
            if (loginDto == null) throw new ArgumentNullException(nameof(loginDto));

            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) return ServiceResult<TokenDto>.Fail("Email or Password is wrong",HttpStatusCode.BadRequest);

            if (!await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                return ServiceResult<TokenDto>.Fail("Email or Password is wrong",HttpStatusCode.BadRequest);
            }
            var token = await _tokenService.CreateToken(user);

            var userRefreshToken = await _refreshTokenRepository.Where(x => x.UserId == user.Id).SingleOrDefaultAsync();

            if (userRefreshToken == null)
            {
                await _refreshTokenRepository.AddAsync(new UserRefreshToken { UserId = user.Id, Code = token.RefreshToken, Expiration = token.RefreshTokenExpiration });
            }
            else
            {
                userRefreshToken.Code = token.RefreshToken;
                userRefreshToken.Expiration = token.RefreshTokenExpiration;
            }

            await _unitOfWork.SaveChangesAsync();

            return ServiceResult<TokenDto>.Success(token, HttpStatusCode.Created);
        }

        public async Task<ServiceResult<TokenDto>> CreateTokenByRefreshToken(string refreshToken)
        {
            var existRefreshToken = await _refreshTokenRepository.Where(x => x.Code == refreshToken).SingleOrDefaultAsync();

            if (existRefreshToken == null)
            {
                return ServiceResult<TokenDto>.Fail("Refresh token not found", HttpStatusCode.NotFound);
            }

            var user = await _userManager.FindByIdAsync(existRefreshToken.UserId);

            if (user == null)
            {
                return ServiceResult<TokenDto>.Fail("User Id not found",HttpStatusCode.NotFound);
            }

            var tokenDto = await _tokenService.CreateToken(user);

            existRefreshToken.Code = tokenDto.RefreshToken;
            existRefreshToken.Expiration = tokenDto.RefreshTokenExpiration;

            await _unitOfWork.SaveChangesAsync();

            return ServiceResult<TokenDto>.Success(tokenDto, HttpStatusCode.Created);
        }

        public async Task<ServiceResult<NoDataDto>> RevokeRefreshToken(string refreshToken)
        {
            var existRefreshToken = await _refreshTokenRepository.Where(x => x.Code == refreshToken).SingleOrDefaultAsync();
            if (existRefreshToken == null)
            {
                return ServiceResult<NoDataDto>.Fail("Refresh token not found", HttpStatusCode.NotFound);
            }

            _refreshTokenRepository.Remove(existRefreshToken);

            await _unitOfWork.SaveChangesAsync();

            return ServiceResult<NoDataDto>.Success(HttpStatusCode.NoContent);
        }
    }
}
