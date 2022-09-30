using CleanTemplate.Application.Authentication.Command.Register;
using CleanTemplate.Application.Authentication.Queries.Login;
using CleanTemplate.Application.Services.Authentication.Common;
using CleanTemplate.Contracts.Authentication;
using Mapster;

namespace CleanTemplate.WebApi.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<LoginRequest, LoginQuery>();
        config.NewConfig<RegisterCommand, RegisterRequest>();
        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest.Token, src => src.Token)
            .Map(dest => dest, src => src.User);
    }
}