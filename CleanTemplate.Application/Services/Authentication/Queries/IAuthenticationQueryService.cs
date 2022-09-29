using CleanTemplate.Application.Services.Authentication.Common;
using ErrorOr;

namespace CleanTemplate.Application.Services.Authentication.Queries
{
    public interface IAuthenticationQueryService
    {
        ErrorOr<AuthenticationResult> Login(string email, string password);
    }
}
