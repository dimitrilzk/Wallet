using System.ComponentModel.DataAnnotations;

namespace Wallet.Application.DTOs.Auth
{
    public sealed class RegisterRequestDto
    {
        [Required, MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required, MinLength(8)]
        public string Password { get; set; } = string.Empty;
    }
}
