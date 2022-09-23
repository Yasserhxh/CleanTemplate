using CleanTemplate.Application.Authentication;
using CleanTemplate.Application.Common.Errors;
using CleanTemplate.Contracts.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneOf;

namespace CleanTemplate.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("Register")]
        public IActionResult Register(RegisterRequest request)
        {
            OneOf<AuthenticationResult, DuplicateEmailError> RegisterResult = _authenticationService.Register(request.FirstName, request.LastName, request.Email, request.Password);
            return RegisterResult.Match(
                authResult => Ok(MapAuthResult(authResult)),
                _=> Problem(statusCode: StatusCodes.Status409Conflict, title: "Email already exists.")
                );
        }

        private static AuthenticationResponse MapAuthResult(AuthenticationResult result)
        {
            return new AuthenticationResponse(result.user.Id,
                result.user.FirstName, 
                result.user.LastName,
                result.user.Email, result.Token);
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginRequest request)
        {
            var result = _authenticationService.Login(request.Email, request.Password);
            var response = new AuthenticationResponse(result.user.Id, result.user.FirstName, result.user.LastName, result.user.Email, result.Token);
            return Ok(response);
        }
    }
}
