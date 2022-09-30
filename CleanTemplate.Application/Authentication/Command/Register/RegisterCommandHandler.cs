using CleanTemplate.Application.Authentication.Command.Register.Persistence;
using CleanTemplate.Application.Common.Interfaces;
using CleanTemplate.Application.Common.Interfaces.Authentication;
using CleanTemplate.Application.Services.Authentication.Common;
using CleanTemplate.Domain.Common.Errors;
using CleanTemplate.Domain.Entities;
using MediatR;
using ErrorOr;


namespace CleanTemplate.Application.Authentication.Command.Register;

public class RegisterCommandHandler : 
    IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    //private readonly IUserRepository _userRepository;
    private readonly IRegisterCommandPersisetence _registerCommandPersisetence;
    public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository, IRegisterCommandPersisetence registerCommandPersisetence)
    {        

        _jwtTokenGenerator = jwtTokenGenerator;
        //_userRepository = userRepository;
        _registerCommandPersisetence = registerCommandPersisetence;
    }

    public Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        // Check if user already exists
        if (_registerCommandPersisetence.GetUserByEmail(command.Email) is not null)
            return Task.FromResult<ErrorOr<AuthenticationResult>>(Errors.User.DuplicateEmail);

        // Create user
        var user = new User
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            Password = command.Password

        };
        _registerCommandPersisetence.Add(user); 
            

        // Generate new JWT Token

        var token = _jwtTokenGenerator.GenerateToken(user);
        return Task.FromResult<ErrorOr<AuthenticationResult>>(new AuthenticationResult(user, token));
    }
}