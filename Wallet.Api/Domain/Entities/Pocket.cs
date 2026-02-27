using Wallet.Api.Domain.Enums;

namespace Wallet.Api.Domain.Entities
{
    public abstract class Pocket : BaseEntity
    {
        protected Pocket(Guid createdByUserId, string pocketName) : base(createdByUserId)
        {
            UserId = createdByUserId;
            Name = pocketName;
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid WalletId { get; set; }
        public string Name { get; set; }
        public PocketType Type { get; set; }
        public bool IsCustom { get; set; }
        public bool IsRecurring { get; set; }

        public decimal TotalBudget => Transactions?.Sum(t => t.Amount) ?? 0;

        public Wallet Wallet { get; set; }
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
