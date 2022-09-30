using System.Configuration;
using CleanTemplate.Application;
using CleanTemplate.Infrastructure;
using CleanTemplate.Persistence;
using CleanTemplate.WebApi;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.
    builder.Services.AddControllers();
    builder.Services
        .AddPersistence(builder.Configuration)
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
}

var app = builder.Build();
{
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();

}


