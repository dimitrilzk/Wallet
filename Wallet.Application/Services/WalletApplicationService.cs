using Wallet.Domain.Entities;
using Wallet.Application.DTOs.Wallet;
using Wallet.Application.Interfaces;
using Wallet.Application.DTOs.Pocket;

namespace Wallet.Application.Services
{
    public class WalletApplicationService : IWalletApplicationService
    {
        private readonly IWalletRepository walletRepository;
        private readonly IUnitOfWork unitOfWork;

        public WalletApplicationService(IWalletRepository walletRepository, IUnitOfWork unitOfWork)
        {
            this.walletRepository = walletRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<WalletResponseDto> GetOrCreateWalletAsync(Guid userId, int year)
        {
            var wallet = await walletRepository.GetByUserAndYearAsync(userId, year);

            if (wallet is null)
            {
                wallet = AnnualWallet.CreateForYear(userId, year);

                await walletRepository.AddAsync(wallet);
                await unitOfWork.SaveChangesAsync();
            }

            return new WalletResponseDto
            {
                Id = wallet.Id,
                Year = wallet.Year,
                WalletStatus = wallet.WalletStatus,
                Pockets = wallet.Pockets.Select(p => new PocketsInWalletResponseDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Role = p.Role,
                    DefaultBalanceSource = p.DefaultBalanceSource,
                    IsFixedBudget = p.IsFixedBudget,
                    EffectivePocketBalance = p.EffectivePocketBalance
                })
                .ToList() 
            };
        }
    }
}
