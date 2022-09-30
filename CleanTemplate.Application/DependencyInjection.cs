using System.Reflection;
using CleanTemplate.Application.Common.Behaviors;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using FluentValidation;

namespace CleanTemplate.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(typeof(DependencyInjection).Assembly);
            services.AddScoped(
                typeof(IPipelineBehavior<, >),
                typeof(ValidateBehavior<, >)
                );
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;

        }
    }
}
