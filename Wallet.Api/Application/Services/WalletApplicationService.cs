using Wallet.Api.Application.Interfaces;
using Wallet.Api.Domain.Entities;
using Wallet.Api.Domain.Enums;
using Wallet.Api.DTOs.Wallet;
using Wallet.Api.Infrastructure.Persistence;

namespace Wallet.Api.Application.Services
{
    public class WalletApplicationService
    {
        private readonly IWalletRepository walletRepository;
        private readonly AppDbContext dbContext;

        public WalletApplicationService(IWalletRepository walletRepository, AppDbContext dbContext)
        {
            this.walletRepository = walletRepository;
            this.dbContext = dbContext;
        }

        public async Task<WalletResponseDto> GetOrCreateWalletAsync(Guid userId, int year)
        {
            var wallet = await walletRepository.GetByUserAndYearAsync(userId, year);

            if (wallet == null)
            {
                WalletStatus wStatus = WalletStatus.Planned;

                if (year == DateTime.UtcNow.Year)
                {
                    wStatus = WalletStatus.Active;
                }
                else if (year > DateTime.UtcNow.Year)
                {
                    wStatus = WalletStatus.Planned;
                }
                else if (year < DateTime.UtcNow.Year)
                {
                    wStatus = WalletStatus.Closed;
                }

                wallet = new AnnualWallet(userId, year, wStatus);

                await walletRepository.AddAsync(wallet);
                await dbContext.SaveChangesAsync();
            }

            return new WalletResponseDto
            {
                Year = wallet.Year,
                WalletStatus = wallet.WalletStatus,
                Pockets = wallet.Pockets.ToList() //TODO dto per le pocket meglio non esporre l'entity completa
            };
        }
    }
}
