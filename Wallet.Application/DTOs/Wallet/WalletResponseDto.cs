using Wallet.Domain.Entities;
using Wallet.Domain.Enums;

namespace Wallet.Application.DTOs.Wallet
{
    public sealed class WalletResponseDto
    {
        public Guid Id { get; set; }
        public int Year { get; set; }
        public WalletStatus WalletStatus { get; set; }
        public List<Pocket> Pockets { get; set; } = new List<Pocket>();
    }
}
