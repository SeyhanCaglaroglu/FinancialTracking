using FinancialTracking.Application.Features.Users.Client;
using FinancialTracking.Application.Features.Users.Dtos;
using FinancialTracking.Domain.Configuration;
using FinancialTracking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Application.Contracts.Auth
{
    public interface ITokenService
    {
        Task<TokenDto> CreateToken(User user);
        ClientTokenDto CreateClientToken(Client client);
    }
}
