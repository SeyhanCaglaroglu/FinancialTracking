using FinancialTracking.Application.Contracts.Auth;
using FinancialTracking.Application.Features.Users.Client;
using FinancialTracking.Application.Features.Users.Dtos;
using FinancialTracking.Auth.Options;
using FinancialTracking.Domain.Configuration;
using FinancialTracking.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Auth.Services
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<User> _userManager;
        private readonly CustomTokenOption _customTokenOption;
        public TokenService(UserManager<User> userManager, IOptions<CustomTokenOption> customTokenOption)
        {
            _userManager = userManager;
            _customTokenOption = customTokenOption.Value;
        }

        private string CreateRefreshToken()
        {
            var numberByte = new Byte[32];

            using var rnd = RandomNumberGenerator.Create();

            rnd.GetBytes(numberByte);

            return Convert.ToBase64String(numberByte);
        }

        private async Task<IEnumerable<Claim>> GetClaims(User User, List<string> audiences)
        {
            var userRoles = await _userManager.GetRolesAsync(User);

            var userList = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,User.Id),
                new Claim(JwtRegisteredClaimNames.Email,User.Email),
                new Claim(ClaimTypes.Name,User.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                
            };

            userList.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
            userList.AddRange(userRoles.Select(x => new Claim(ClaimTypes.Role, x)));

            return userList;
        }

        private IEnumerable<Claim> GetClaimsByClient(Client client)
        {
            var claims = new List<Claim>();
            claims.AddRange(client.Audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));

            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());
            new Claim(JwtRegisteredClaimNames.Sub, client.Id.ToString());

            return claims;
        }


        public ClientTokenDto CreateClientToken(Client client)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(_customTokenOption.AccessTokenExpiration);

            var securityKey = SignInService.GetSymmetricSecurityKey(_customTokenOption.SecurityKey);

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken
                (
                    issuer: _customTokenOption.Issuer,
                    expires: accessTokenExpiration,
                    notBefore: DateTime.Now,
                    claims: GetClaimsByClient(client),
                    signingCredentials: signingCredentials
                );

            var handler = new JwtSecurityTokenHandler();

            var token = handler.WriteToken(jwtSecurityToken);

            var tokenDto = new ClientTokenDto(token, accessTokenExpiration);

            return tokenDto;
        }

        public async Task<TokenDto> CreateToken(User User)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(_customTokenOption.AccessTokenExpiration);
            var refreshTokenExpiration = DateTime.Now.AddMinutes(_customTokenOption.RefreshTokenExpiration);

            var securityKey = SignInService.GetSymmetricSecurityKey(_customTokenOption.SecurityKey);

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken
                (
                    issuer: _customTokenOption.Issuer,
                    expires: accessTokenExpiration,
                    notBefore: DateTime.Now,
                    claims: await GetClaims(User, _customTokenOption.Audience),
                    signingCredentials: signingCredentials
                );

            var handler = new JwtSecurityTokenHandler();

            var token = handler.WriteToken(jwtSecurityToken);

            var tokenDto = new TokenDto(token, accessTokenExpiration, CreateRefreshToken(), refreshTokenExpiration);


            return tokenDto;
        }
    }
}
