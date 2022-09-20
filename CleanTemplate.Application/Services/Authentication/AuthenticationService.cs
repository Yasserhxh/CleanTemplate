using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTemplate.Application.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        public AuthenticationResult Login(string Email, string Password)
        {
            return new AuthenticationResult(Guid.NewGuid(), "TestFirst", "TestLast", Email, "Token");
        }

        public AuthenticationResult Register(string FirstName, string LastName, string Email, string Password)
        {
            return new AuthenticationResult(Guid.NewGuid(), FirstName, LastName, Email, "Token");
        }
    }
}
