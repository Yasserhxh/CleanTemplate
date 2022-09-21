using CleanTemplate.Application.Common.Interfaces;
using CleanTemplate.Application.Common.Interfaces.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CleanTemplate.Infrastructure.Authentication
{

    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly JwtSettings jwtSettings;

        public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtOptions)
        {
            _dateTimeProvider = dateTimeProvider;
            jwtSettings = jwtOptions.Value;
        }

        public string GenerateToken(Guid userId, string FirstName, string LastName)
        {
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName, FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, LastName),
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString())
            };
            var securityToken = new JwtSecurityToken(claims: claims,
                                                     signingCredentials: signingCredentials,
                                                     issuer: jwtSettings.Issuer,
                                                     expires: _dateTimeProvider.UtcNow.AddMinutes(jwtSettings.ExpirationTimeInMinutes),
                                                     audience: jwtSettings.Audience);
            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}
