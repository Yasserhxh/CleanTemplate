using CleanTemplate.Application.Authentication.Queries.Login.Persistence;
using CleanTemplate.Domain.Context;
using CleanTemplate.Persistence.Queries.Login;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanTemplate.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, 
        ConfigurationManager configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("CleanArch_db") ?? throw new InvalidOperationException()));
        services.AddScoped<ILoginQueryPersistence, LoginQueryPersistence>();

        
        return services;
    }
}