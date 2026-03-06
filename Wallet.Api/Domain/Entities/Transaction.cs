using Wallet.Api.Domain.Enums;
using Wallet.Api.Domain.Interfaces;

namespace Wallet.Api.Domain.Entities
{
    public class Transaction : IEntity, IAuditable, ISoftDeletable
    {
        protected Transaction() { }
        public Transaction(Guid createdByUserId,
                           Guid pocketId,
                           BalanceSource impactedBalance,
                           decimal amount,
                           bool isIncome,
                           DateTime transactionDate,
                           Guid? categoryId = null,
                           string? description = null)
        {
            Id = Guid.NewGuid();
            UserId = createdByUserId;
            PocketId = pocketId;
            ImpactedBalance = impactedBalance;
            Amount = amount;
            IsIncome = isIncome;
            TransacrionDate = transactionDate;
            CategoryId = categoryId;
            Description = description;
        }

        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Guid PocketId { get; private set; }
        public Guid? CategoryId { get; private set; }
        public bool IsIncome { get; set; }
        public decimal Amount { get; set; }
        public decimal SignedAmount => IsIncome ? Amount : -Amount;
        public string? Description { get; set; }
        public DateTime TransacrionDate { get; set; }
        public BalanceSource ImpactedBalance { get; set; } // BankLiquidity - CashLiquidity - BankSavings - CashSavings 

        // IAuditable
        public DateTime CreatedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }

        // ISoftDeletable
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }

        // Navigation
        public Pocket Pocket { get; set; }
        public Category Category { get; set; }
    }
}
