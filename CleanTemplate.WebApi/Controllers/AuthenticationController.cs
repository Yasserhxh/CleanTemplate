using CleanTemplate.Application.Authentication.Command.Register;
using CleanTemplate.Application.Authentication.Queries.Login;
using CleanTemplate.Contracts.Authentication;
using CleanTemplate.Domain.Common.Errors;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace CleanTemplate.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticationController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;
        public AuthenticationController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var command = _mapper.Map<RegisterCommand>(request);
            //var command = new RegisterCommand(request.FirstName, request.LastName, request.Email, request.Password);
            var result = await _mediator.Send(command);
            return result.Match(
                registerResult => Ok(_mapper.Map<AuthenticationResponse>(registerResult)),
                Problem);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            //var query = new LoginQuery(request.Email, request.Password);
            var query = _mapper.Map<LoginQuery>(request);
            
            var result = await _mediator.Send(query);
            if (result.IsError && result.FirstError == Errors.Authentication.InvalidCredentials)
                return Problem(
                    statusCode: StatusCodes.Status401Unauthorized, 
                    title: result.FirstError.Description
                );
            return result.Match(
                loginResult => Ok(_mapper.Map<AuthenticationResponse>(loginResult)),
                Problem
                );
            
        }
    }
}
