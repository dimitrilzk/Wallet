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
            PocketFundingSource fundingSource,
            decimal? plannedAmount = null,
            bool isRecurring = false)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            WalletId = walletId;
            Name = name;
            Role = role;
            FundingSource = fundingSource;
            PlannedAmount = plannedAmount;
            IsRecurring = isRecurring;
        }

        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Guid WalletId { get; private set; }

        public string Name { get; private set; }

        // Expense - Income - SavingGoal - Investment 
        public PocketRole Role { get; private set; } 

        // BankLiquidity - CashLiquidity - BankSavings - CashSavings 
        public PocketFundingSource FundingSource { get; private set; } 

        // Budget pianificato/obbiettivo della pocket
        public decimal? PlannedAmount { get; private set; }

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
        public Wallet Wallet { get; set; }
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

        public void ChangeName(string name)
        {
            Name = name;
        }

        public void ChangeRole(PocketRole role)
        {
            Role = role; 
        }

        public void ChangeFundingSource(PocketFundingSource fundingSource)
        {
            FundingSource = fundingSource;
        }

        public void ChangePlannedAmount(decimal plannedAmount)
        {
            PlannedAmount = plannedAmount; 
        }

        public void ChangeIsRecurring(bool isRecurring)
        {
            IsRecurring = isRecurring; 
        }
    }
}
