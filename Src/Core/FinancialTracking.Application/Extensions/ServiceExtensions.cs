using FinancialTracking.Application.Features.Budgets.Services;
using FinancialTracking.Application.Features.Categories.Services;
using FinancialTracking.Application.Features.Goals.Services;
using FinancialTracking.Application.Features.RecurringTransactions.Services;
using FinancialTracking.Application.Features.Transactions.Services;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Application.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services,IConfiguration configuration)
        {
            services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IBudgetService, BudgetService>();
            services.AddScoped<IGoalService, GoalService>();
            services.AddScoped<IRecurringTransactionService, RecurringTransactionService>();

            services.AddFluentValidationAutoValidation();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());


            return services;
        }
    }
}
