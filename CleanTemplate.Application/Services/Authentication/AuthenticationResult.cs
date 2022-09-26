using CleanTemplate.Domain.Entities;

namespace CleanTemplate.Application.Services.Authentication
{
    public record AuthenticationResult(
        User User,
        string Token);
   
}
