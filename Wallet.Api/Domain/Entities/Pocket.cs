using Wallet.Api.Domain.Enums;
using Wallet.Api.Domain.Interfaces;

namespace Wallet.Api.Domain.Entities
{
    public class Pocket : IEntity, IAuditable, ISoftDeletable
    {
        protected Pocket() { }

        public Pocket(
            Guid userId,
            Guid walletId,
            string name,
            PocketRole role,
            BalanceSource defaultSource,
            bool isRecurring = false,
            decimal? plannedAmount = null)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            WalletId = walletId;
            Name = name;
            Role = role;
            DefaultBalanceSource = defaultSource;
            IsRecurring = isRecurring;
            PlannedAmount = plannedAmount;
        }

        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Guid WalletId { get; private set; }

        public string Name { get; private set; }
        public PocketRole Role { get; private set; } // Expense - Income - SavingGoal - Investment
        public BalanceSource DefaultBalanceSource { get; private set; } // BankLiquidity - CashLiquidity - BankSavings - CashSavings 
        public decimal? PlannedAmount { get; private set; } // Budget pianificato/obbiettivo della pocket
        public bool IsRecurring { get; private set; }

        // Saldo attuale della pocket (derivato o mantenuto)
        public decimal CurrentBalance => Transactions?.Sum(t => t.SignedAmount) ?? 0m;

        // IAuditable
        public DateTime CreatedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }

        // ISoftDeletable
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }

        // Navigation
        public AnnualWallet Wallet { get; set; }
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

        public void ChangeName(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
        public void ChangeRole(PocketRole role) => Role = role;
        public void ChangeDefaultBalanceSource(BalanceSource balanceSource) => DefaultBalanceSource = balanceSource;
        public void ChangePlannedAmount(decimal plannedAmount) => PlannedAmount = plannedAmount;
        public void ChangeIsRecurring(bool isRecurring) => IsRecurring = isRecurring;
    }
}
