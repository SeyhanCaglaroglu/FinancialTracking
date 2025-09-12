using Asp.Versioning;

namespace FinancialTracking.API.Extensions
{
    public static class VersioningExtensions
    {
        public static IServiceCollection AddVersioningExt(this IServiceCollection services)
        {
            services.AddApiVersioning(opt =>
            {
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.ReportApiVersions = true;
                opt.ApiVersionReader = ApiVersionReader
                    .Combine(
                        new UrlSegmentApiVersionReader(),
                        new QueryStringApiVersionReader("x-version"),
                        new HeaderApiVersionReader("x-api-version")
                    );
            }).AddMvc();

            return services;
        }
    }
}
