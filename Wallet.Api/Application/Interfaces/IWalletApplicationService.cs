using Wallet.Api.DTOs.Wallet;

namespace Wallet.Api.Application.Interfaces
{
    public interface IWalletApplicationService
    {
        Task<WalletResponseDto> GetOrCreateWalletAsync(Guid userId, int year);
    }
}
