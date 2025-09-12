using FinancialTracking.Application.Features.Categories;
using FinancialTracking.Domain.Entities;
using FinancialTracking.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Persistence.Features.Categories
{
    public class CategoryRepository(FTDbContext context) : GenericRepository<Category, int>(context), ICategoryRepository
    {
        public Task<Category> GetCategoryByNameAsync(string name, string userId)
    => Context.Categories.SingleOrDefaultAsync(x => x.Name == name && x.UserId == userId);

        public Task<Category> GetCategoryInTransactionsByNameAsync(string name, string userId)=> Context.Categories.Include(x=>x.Transactions).Include(x=>x.RecurringTransactions).SingleOrDefaultAsync(x => x.Name == name && x.UserId == userId);
    }
}
