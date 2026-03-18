using Wallet.Api.Domain.Entities;

namespace Wallet.Api.Application.Auth
{
    public interface IJwtTokenGenerator
    {
        JwtTokenResult GenerateTokenJwt(AppUser user);
    }

    public sealed class JwtTokenResult
    {
        public string AccessToken { get; init; } = string.Empty;
        public DateTime ExpiresAtUtc { get; init; }
    }
}
