using CleanTemplate.Application.Common.Errors;
using CleanTemplate.Application.Common.Interfaces;
using CleanTemplate.Application.Common.Interfaces.Authentication;
using CleanTemplate.Domain.Entities;
using FluentResults;

namespace CleanTemplate.Application.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public AuthenticationResult Login(string email, string password)
        {
            // Check User exists
            if(_userRepository.GetUserByEmail(email) is not { } user)
                throw new Exception("User does not exist.");


            // Validate password correct

            if (user.Password != password)
                throw new Exception("Password is invalid");

            // Create JWT token
            var token = _jwtTokenGenerator.GenerateToken(user);


            return new AuthenticationResult(
                user,
                token);
        }

        public Result<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
        {
            // Check if user already exists
            if (_userRepository.GetUserByEmail(email) is not null)
                return Result.Fail<AuthenticationResult>(new [] {new DuplicateEmailError()});

            // Create user
            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password

            };
            _userRepository.Add(user); 
            

            // Generate new JWT Token

            var token = _jwtTokenGenerator.GenerateToken(user);
            return new AuthenticationResult(user, token);
        }
    }
}
