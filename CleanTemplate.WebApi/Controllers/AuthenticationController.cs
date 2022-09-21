using CleanTemplate.Application.Authentication;
using CleanTemplate.Contracts.Authentication;
using CleanTemplate.WebApi.Filters;
using Microsoft.AspNetCore.Http;
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
            var result = _authenticationService.Register(request.FirstName, request.LastName, request.Email, request.Password);
            var response = new AuthenticationResponse(result.user.Id,result.user.FirstName, result.user.LastName, result.user.Email, result.Token);
            return Ok(response);
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
