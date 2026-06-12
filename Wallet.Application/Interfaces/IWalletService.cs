using Wallet.Application.DTOs.Wallet;

namespace Wallet.Application.Interfaces
{
    public interface IWalletService
    {
        Task<WalletResponseDto> GetOrCreateWalletAsync(Guid userId, int year);
    }
}
