using FinancialTracking.API.Filters;
using FluentValidation.AspNetCore;

namespace FinancialTracking.API.Extensions
{
    public static class ControllerExtensions
    {
        public static IServiceCollection AddControllersWithValidation(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add<FluentValidationFilter>();
                options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
            }).AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.PropertyNameCaseInsensitive = true;

            });
                    

            return services;
        }
    }
}
