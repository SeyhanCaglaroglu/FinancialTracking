using FluentValidation.AspNetCore;

namespace FinancialTracking.API.Extensions
{
    public static class ControllerExtensions
    {
        public static IServiceCollection AddControllersWithValidation(this IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            }).AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblyContaining<Program>();
                options.DisableDataAnnotationsValidation = true;
            });
                    

            return services;
        }
    }
}
