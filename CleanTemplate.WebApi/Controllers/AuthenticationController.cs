using CleanTemplate.Application.Authentication.Command.Register;
using CleanTemplate.Application.Authentication.Queries.Login;
using CleanTemplate.Application.Services.Authentication.Common;
using CleanTemplate.Contracts.Authentication;
using CleanTemplate.Domain.Common.Errors;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace CleanTemplate.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticationController : ApiController
    {
        private readonly ISender _mediator;
        public AuthenticationController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var command = new RegisterCommand(request.FirstName, request.LastName, request.Email, request.Password);
            var result = await _mediator.Send(command);
            return result.Match(
                registerResult => Ok(MapAuthResult(registerResult)),
                Problem);
        }

        private static AuthenticationResponse MapAuthResult(AuthenticationResult result)
        {
            return new AuthenticationResponse(result.User.Id,
                result.User.FirstName, 
                result.User.LastName,
                result.User.Email, result.Token);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var query = new LoginQuery(request.Email, request.Password);
            var result = await _mediator.Send(query);
            if (result.IsError && result.FirstError == Errors.Authentication.InvalidCredentials)
                return Problem(
                    statusCode: StatusCodes.Status401Unauthorized, 
                    title: result.FirstError.Description
                );
            return result.Match(
                loginResult => Ok(MapAuthResult(loginResult)),
                Problem
                );
            
        }
    }
}
