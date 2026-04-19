using Wallet.Domain.Entities;

namespace Wallet.Application.Interfaces
{
    public interface IWalletRepository
    {
        Task<AnnualWallet?> GetByUserAndYearAsync(Guid userId, int year);
        Task AddAsync(AnnualWallet wallet);
    }
}
