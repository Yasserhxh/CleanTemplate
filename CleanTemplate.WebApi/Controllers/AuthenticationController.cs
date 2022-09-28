using CleanTemplate.Application.Services.Authentication;
using CleanTemplate.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;
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
            var registerResult = _authenticationService.Register(request.FirstName, request.LastName, request.Email, request.Password);
            //if (registerResult.IsSuccess)
                //return Ok(MapAuthResult(registerResult.Value));
            //var firstError = registerResult.Errors[0];
            //return firstError is DuplicateEmailError ? Problem(statusCode: StatusCodes.Status409Conflict, title: "Email already exists.") : Problem();
            return registerResult.Match(
                registerResult => Ok(MapAuthResult(registerResult)),
                _=> Problem(statusCode:StatusCodes.Status409Conflict,
                    title: "User already exists")
                );
        }

        private static AuthenticationResponse MapAuthResult(AuthenticationResult result)
        {
            return new AuthenticationResponse(result.User.Id,
                result.User.FirstName, 
                result.User.LastName,
                result.User.Email, result.Token);
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginRequest request)
        {
            var result = _authenticationService.Login(request.Email, request.Password);
            var response = new AuthenticationResponse(result.User.Id, result.User.FirstName, result.User.LastName, result.User.Email, result.Token);
            return Ok(response);
        }
    }
}
