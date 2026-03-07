using Wallet.Api.Domain.Enums;
using Wallet.Api.Domain.Interfaces;

namespace Wallet.Api.Domain.Entities
{
    public class Wallet : IEntity, IAuditable, ISoftDeletable
    {
        protected Wallet() { }
        public Wallet(Guid userId, int year, WalletStatus walletStatus)
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
        public AppUser User { get; set; }
        public ICollection<Pocket> Pockets { get; set; } = new List<Pocket>();

        public void ChangeYear(int newYear) => Year = newYear;
        public void ChangeStatus(WalletStatus walletStatus) => WalletStatus = walletStatus;
    }
}
