using CleanTemplate.WebApi.Common.Errors;
using CleanTemplate.WebApi.Common.Mapping;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace CleanTemplate.WebApi
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<ProblemDetailsFactory, ProblemDetailsFact>();
            services.AddMappings();
            return services;

        }
    }
}
