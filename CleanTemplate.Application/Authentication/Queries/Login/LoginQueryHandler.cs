using CleanTemplate.Application.Authentication.Queries.Login.Persistence;
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
    //private readonly IUserRepository _userRepository;
    private readonly ILoginQueryPersistence _loginQueryPersistence;
    public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, ILoginQueryPersistence loginQueryPersistence)
    {
        //_userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
        _loginQueryPersistence = loginQueryPersistence;
    }

    public Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        // Check User exists
        if(_loginQueryPersistence.Login(query.Email) is not { } user)
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