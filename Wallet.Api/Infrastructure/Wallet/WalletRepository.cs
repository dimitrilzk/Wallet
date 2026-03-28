using Wallet.Api.Application.Interfaces;
using Wallet.Api.Domain.Entities;

namespace Wallet.Api.Infrastructure.Wallet
{
    public class WalletRepository : IWalletRepository
    {
        public Task AddAsync(AnnualWallet wallet)
        {
            throw new NotImplementedException();
        }

        public Task<AnnualWallet?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<AnnualWallet?> GetByUserAndYearAsync(Guid userId, int year)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(AnnualWallet wallet)
        {
            throw new NotImplementedException();
        }
    }
}
