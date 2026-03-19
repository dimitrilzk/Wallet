using System.ComponentModel.DataAnnotations;

namespace Wallet.Api.DTOs.Auth
{
    public sealed class RegisterRequestDto
    {
        [Required, EmailAddress]
        public string FirstName { get; set; } = string.Empty;
        [Required, MinLength(8)]
        public string Email { get; set; } = string.Empty;
        [Required, MaxLength(100)]
        public string Password { get; set; } = string.Empty;
    }
}
