using CleanTemplate.Application.Services.Authentication.Common;
using MediatR;
using ErrorOr;
namespace CleanTemplate.Application.Authentication.Queries.Login;

public record LoginQuery(
    string Email,
    string Password
) : IRequest<ErrorOr<AuthenticationResult>>;
