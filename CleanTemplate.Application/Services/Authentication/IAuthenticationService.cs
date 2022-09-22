using CleanTemplate.Application.Common.Errors;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTemplate.Application.Authentication
{
    public interface IAuthenticationService
    {
        OneOf<AuthenticationResult, DuplicateEmailError> Register(string FirstName, string LastName, string Email, string Password);
        AuthenticationResult Login(string Email, string Password);
    }
}
