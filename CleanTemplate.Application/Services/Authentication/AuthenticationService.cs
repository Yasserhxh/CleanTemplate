using CleanTemplate.Application.Common.Interfaces.Authentication;
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

        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public AuthenticationResult Login(string Email, string Password)
        {
            return new AuthenticationResult(Guid.NewGuid(), "TestFirst", "TestLast", Email, "Token");
        }

        public AuthenticationResult Register(string FirstName, string LastName, string Email, string Password)
        {
            // Check if user already exists
            // Create user
            // Generate new JWT Token
            var userID = Guid.NewGuid();
            var token = _jwtTokenGenerator.GenerateToken(userID, FirstName, LastName);
            return new AuthenticationResult(userID, FirstName, LastName, Email, token);
        }
    }
}
