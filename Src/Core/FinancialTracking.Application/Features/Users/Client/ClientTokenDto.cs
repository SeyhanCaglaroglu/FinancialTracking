using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Application.Features.Users.Client
{
    public record ClientTokenDto(string AccessToken, DateTime AccessTokenExpiration);
}
