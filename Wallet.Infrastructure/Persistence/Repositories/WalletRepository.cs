using Microsoft.EntityFrameworkCore;
using Wallet.Application.Interfaces;
using Wallet.Domain.Entities;

namespace Wallet.Infrastructure.Persistence.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        private readonly AppDbContext dbContext;

        public WalletRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddAsync(AnnualWallet wallet)
        {
            await dbContext.Wallets.AddAsync(wallet);   
        }

        public async Task<AnnualWallet?> GetByUserAndYearAsync(Guid userId, int year)
        {
            return await dbContext.Wallets
                .Where(w => w.UserId == userId && w.Year == year)
                .Include(w => w.Pockets)
                .FirstOrDefaultAsync();
        }
    }
}
