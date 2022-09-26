using Microsoft.Extensions.DependencyInjection;
using CleanTemplate.Application.Services.Authentication;

namespace CleanTemplate.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            return services;

        }
    }
}
