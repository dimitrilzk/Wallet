using Wallet.Application.DTOs.Wallet;

namespace Wallet.Application.Interfaces
{
    public interface IWalletApplicationService
    {
        Task<WalletResponseDto> GetOrCreateWalletAsync(Guid userId, int year);
    }
}
