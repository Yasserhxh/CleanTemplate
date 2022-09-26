using CleanTemplate.Domain.Entities;

namespace CleanTemplate.Application.Common.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
