using FinancialTracking.Application.Extensions;
using FinancialTracking.Auth.Extensions;
using FinancialTracking.Auth.Options;
using FinancialTracking.Domain.Configuration;
using FinancialTracking.Persistence.Extensions;

namespace FinancialTracking.API.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Clients konfigürasyonunu ekle
            services.Configure<List<Client>>(configuration.GetSection("Clients"));

            // TokenOptions al
            var tokenOptions = configuration.GetSection("TokenOptions").Get<CustomTokenOption>();

            services.AddApplication(configuration)
                    .AddPersistence(configuration)
                    .AddAuthExt(tokenOptions!);

            return services;
        }
    }
}
