using CleanTemplate.Domain.Entities;

namespace CleanTemplate.Application.Services.Authentication.Common
{
    public record AuthenticationResult(
        User User,
        string Token);
   
}
