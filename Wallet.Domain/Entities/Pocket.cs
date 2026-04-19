using Wallet.Domain.Enums;
using Wallet.Domain.Interfaces;

namespace Wallet.Domain.Entities
{
    public class Pocket : IEntity, IAuditable, ISoftDeletable
    {
        protected Pocket() { }

        public Pocket(
            Guid walletId,
            string name,
            PocketRole role,
            BalanceSource defaultSource,
            bool isFixedBudget = false,
            decimal? manualOverrideAmount = null)
        {
            Id = Guid.NewGuid();
            WalletId = walletId;
            Name = name;
            Role = role;
            DefaultBalanceSource = defaultSource;
            IsFixedBudget = isFixedBudget;
            ManualOverrideAmount = manualOverrideAmount;
        }

        public Guid Id { get; private set; }
        public Guid WalletId { get; private set; }

        public string Name { get; private set; }
        public PocketRole Role { get; private set; } // Expense - Income - SavingGoal - Investment
        public BalanceSource DefaultBalanceSource { get; private set; } // BankLiquidity - CashLiquidity - BankSavings - CashSavings 
        public bool IsFixedBudget { get; private set; } // UX: Segna questa pocket come Budget fisso se vuoi considerare il suo importo come un costo/risparmio fisso da togliere dal tuo reddito principale, a prescindere dalla somma precisa delle transazioni.
        public decimal? ManualOverrideAmount { get; private set; } // Valore arrotondato e sovrascrivente del

        // Saldo attuale della pocket (derivato o mantenuto)
        public decimal CurrentBalance => Transactions?.Sum(t => t.SignedAmount) ?? 0m;
        public decimal? EffectivePocketBalance => ManualOverrideAmount ?? CurrentBalance;

        // IAuditable
        public DateTime CreatedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }

        // ISoftDeletable
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }

        // Navigation
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

        public void ChangeName(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
        public void ChangeRole(PocketRole role) => Role = role;
        public void ChangeDefaultBalanceSource(BalanceSource balanceSource) => DefaultBalanceSource = balanceSource;
        public void OverrideBudgetAmount(decimal overrideAmount) => ManualOverrideAmount = overrideAmount;
        public void ChangeIsFixedBudget(bool isFixedBudget) => IsFixedBudget = isFixedBudget;
    }
}
