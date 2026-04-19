namespace Wallet.Application.DTOs.Auth
{
    public sealed class AuthResponseDto
    {
        public string AccessToken { get; set; } = string.Empty;
        public DateTime ExpiresAtUtc { get; set; }
        public int ExpiresInSeconds { get; set; }
    }
}
