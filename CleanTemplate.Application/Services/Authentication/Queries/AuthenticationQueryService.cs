using CleanTemplate.Application.Common.Interfaces;
using CleanTemplate.Application.Common.Interfaces.Authentication;
using CleanTemplate.Application.Services.Authentication.Common;
using CleanTemplate.Domain.Common.Errors;
using ErrorOr;

namespace CleanTemplate.Application.Services.Authentication.Queries
{
    public class AuthenticationQueryService : IAuthenticationQueryService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public AuthenticationQueryService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public ErrorOr<AuthenticationResult> Login(string email, string password)
        {
            // Check User exists
            if(_userRepository.GetUserByEmail(email) is not { } user)
                return Errors.Authentication.InvalidCredentials;


            // Validate password correct

            if (user.Password != password)
                return Errors.Authentication.InvalidCredentials;

            // Create JWT token
            var token = _jwtTokenGenerator.GenerateToken(user);


            return new AuthenticationResult(
                user,
                token);
        }
    }
}
