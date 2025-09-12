using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Application.Features.Users.Dtos
{
    public record UserDto(string Id, string UserName, string Email);
}
