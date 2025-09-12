using FinancialTracking.Domain.Entities;
using FinancialTracking.Persistence.Context;
using Microsoft.AspNetCore.Identity;

namespace FinancialTracking.API.Extensions
{
    public static class IdentityExtensions
    {
        public static IServiceCollection AddCustomIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>(opt =>
            {
                opt.User.RequireUniqueEmail = true;
                opt.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<FTDbContext>()
            .AddDefaultTokenProviders();

            return services;
        }
    }
}
