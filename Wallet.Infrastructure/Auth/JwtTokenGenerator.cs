using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Wallet.Domain.Entities;
using Wallet.Application.Auth;
using Wallet.Infrastructure.Configuration;

namespace Wallet.Infrastructure.Auth
{
    public sealed class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtOptions options;

        public JwtTokenGenerator(IOptions<JwtOptions> options)
        {
            this.options = options.Value;
        }

        public JwtTokenResult GenerateTokenJwt(AppUser user)
        {
            var keyBytes = Encoding.UTF8.GetBytes(options.SigningKey);
            var securityKey = new SymmetricSecurityKey(keyBytes);
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()), // NameIdentifier
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), //JWT ID
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new Claim("name", user.FirstName ?? string.Empty)
            };

            var expiresAt = DateTime.UtcNow.AddMinutes(60);

            var jwtToken = new JwtSecurityToken(issuer: options.Issuer,
                                                audience: options.Audience,
                                                claims: claims,
                                                notBefore: DateTime.UtcNow,
                                                expires: expiresAt,
                                                signingCredentials: signingCredentials);

            var handler = new JwtSecurityTokenHandler();
            var tokenString = handler.WriteToken(jwtToken);

            return new JwtTokenResult
            {
                AccessToken = tokenString,
                ExpiresAtUtc = expiresAt
            };
        }
    }
}
