using FinancialTracking.Application.Contracts.Persistence;
using FinancialTracking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Application.Features.Categories
{
    public interface ICategoryRepository: IGenericRepository<Category,int>
    {
        Task<Category> GetCategoryByNameAsync(string name, string userId);
        Task<Category> GetCategoryInTransactionsByNameAsync(string name, string userId);
    }
}
