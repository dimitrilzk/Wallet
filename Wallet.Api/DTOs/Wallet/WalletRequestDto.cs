using System.ComponentModel.DataAnnotations;

namespace Wallet.Api.DTOs.Wallet
{
    public sealed class WalletRequestDto
    {
        [Required]
        public int Year { get; set; }
    }
}
