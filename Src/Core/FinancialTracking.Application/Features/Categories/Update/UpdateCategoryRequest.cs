using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Application.Features.Categories.Update
{
    public record UpdateCategoryRequest(string Name, string userId);

}
