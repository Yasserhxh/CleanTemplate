using CleanTemplate.Application.Common.Interfaces;
using CleanTemplate.Application.Common.Interfaces.Authentication;
using CleanTemplate.Application.Services.Authentication.Common;
using CleanTemplate.Domain.Common.Errors;
using MediatR;
using ErrorOr;

namespace CleanTemplate.Application.Authentication.Queries.Login;

public class LoginQueryHandler :
    IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public LoginQueryHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        // Check User exists
        if(_userRepository.GetUserByEmail(query.Email) is not { } user)
            return Task.FromResult<ErrorOr<AuthenticationResult>>(Errors.Authentication.InvalidCredentials);


        // Validate password correct

        if (user.Password != query.Password)
            return Task.FromResult<ErrorOr<AuthenticationResult>>(Errors.Authentication.InvalidCredentials);

        // Create JWT token
        var token = _jwtTokenGenerator.GenerateToken(user);


        return Task.FromResult<ErrorOr<AuthenticationResult>>(new AuthenticationResult(
            user,
            token));
    }
}