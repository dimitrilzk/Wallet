using Wallet.Api.Domain.Entities;
using Wallet.Api.Domain.Enums;

namespace Wallet.Api.DTOs.Wallet
{
    public sealed class WalletResponseDto
    {
        public int Year { get; set; }
        public WalletStatus WalletStatus { get; set; }
        public List<Pocket> Pockets { get; set; } = new List<Pocket>();
    }
}
