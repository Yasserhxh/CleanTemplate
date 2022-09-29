using CleanTemplate.Application.Services.Authentication.Common;
using ErrorOr;
using MediatR;

namespace CleanTemplate.Application.Authentication.Command.Register;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password
): IRequest<ErrorOr<AuthenticationResult>>;
