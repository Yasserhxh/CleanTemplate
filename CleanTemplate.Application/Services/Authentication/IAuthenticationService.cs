using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTemplate.Application.Authentication
{
    public interface IAuthenticationService
    {
        AuthenticationResult Register(string FirstName, string LastName, string Email, string Password);
        AuthenticationResult Login(string Email, string Password);
    }
}
