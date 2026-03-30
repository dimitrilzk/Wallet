using Wallet.Api.Domain.Entities;

namespace Wallet.Api.Application.Interfaces
{
    public interface IWalletRepository
    {
        Task<AnnualWallet?> GetByUserAndYearAsync(Guid userId, int year);
        Task AddAsync(AnnualWallet wallet);
    }
}
