using CleanTemplate.Application.Common.Errors;
using CleanTemplate.Application.Common.Interfaces;
using CleanTemplate.Application.Common.Interfaces.Authentication;
using CleanTemplate.Domain.Entities;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTemplate.Application.Authentication
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

        public AuthenticationResult Login(string Email, string Password)
        {
            // Check User exists
            if(_userRepository.GetUserByEmail(Email) is not User user)
                throw new Exception("User does not exist.");


            // Validate password correct

            if (user.Password != Password)
                throw new Exception("Password is invalid");

            // Create JWT token
            var token = _jwtTokenGenerator.GenerateToken(user);


            return new AuthenticationResult(
                user,
                token);
        }

        public OneOf<AuthenticationResult, DuplicateEmailError> Register(string FirstName, string LastName, string Email, string Password)
        {
            // Check if user already exists
            if (_userRepository.GetUserByEmail(Email) is not null)
                return new DuplicateEmailError();

            // Create user
            var user = new User
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Password = Password

            };
            _userRepository.Add(user); 
            

            // Generate new JWT Token

            var token = _jwtTokenGenerator.GenerateToken(user);
            return new AuthenticationResult(user, token);
        }
    }
}
