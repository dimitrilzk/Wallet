using Wallet.Api.Domain.Enums;
using Wallet.Api.Domain.Interfaces;

namespace Wallet.Api.Domain.Entities
{
    public class AnnualWallet : IEntity, IAuditable, ISoftDeletable
    {
        protected AnnualWallet() { }
        private AnnualWallet(Guid userId, int year, WalletStatus walletStatus)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Year = year;
            WalletStatus = walletStatus;
        }

        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public int Year { get; private set; }
        public WalletStatus WalletStatus { get; private set; }

        // IAuditable
        public DateTime CreatedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }

        // ISoftDeletable
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }

        // Navigation
        public AppUser User { get; private set; }
        public ICollection<Pocket> Pockets { get; private set; } = new List<Pocket>();

        public void RefreshStatus()
        {
            WalletStatus = ResolveStatusForYear(Year);
        }

        public static AnnualWallet CreateForYear(Guid userId, int year)
        {
            return new AnnualWallet(userId, year, ResolveStatusForYear(year));
        }

        private static WalletStatus ResolveStatusForYear(int year)
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

            return wStatus;
        }
    }
}
