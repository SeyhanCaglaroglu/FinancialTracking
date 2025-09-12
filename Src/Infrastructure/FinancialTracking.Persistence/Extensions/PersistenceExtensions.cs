using FinancialTracking.Application.Contracts.Persistence;
using FinancialTracking.Application.Features.Budgets;
using FinancialTracking.Application.Features.Categories;
using FinancialTracking.Application.Features.Goals;
using FinancialTracking.Application.Features.RecurringTransactions;
using FinancialTracking.Application.Features.RefreshTokens;
using FinancialTracking.Application.Features.Transactions;
using FinancialTracking.Domain.Configuration;
using FinancialTracking.Persistence.Context;
using FinancialTracking.Persistence.Features.Budgets;
using FinancialTracking.Persistence.Features.Categories;
using FinancialTracking.Persistence.Features.Goals;
using FinancialTracking.Persistence.Features.RecurringTransactions;
using FinancialTracking.Persistence.Features.RefreshTokens;
using FinancialTracking.Persistence.Features.Transactions;
using FinancialTracking.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Persistence.Extensions
{
    public static class PersistenceExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FTDbContext>(options =>
            {
                var connectionStrings =
                    configuration.GetSection(ConnectionStringOption.Key).Get<ConnectionStringOption>();

                options.UseSqlServer(connectionStrings!.SqlServer,
                    sqlServerOptionsAction =>
                    {
                        sqlServerOptionsAction.MigrationsAssembly(typeof(PersistenceAssembly).Assembly.FullName);
                    });

                options.AddInterceptors(new AuditDbContextInterceptor());
            });
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IBudgetRepository, BudgetRepository>();
            services.AddScoped<IGoalRepository, GoalRepository>();
            services.AddScoped<IRecurringTransactionRepository, RecurringTransactionRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
