using Wallet.Api.Domain.Enums;
using Wallet.Api.Domain.Interfaces;

namespace Wallet.Api.Domain.Entities
{
    public class Transaction : IEntity, IAuditable, ISoftDeletable
    {
        protected Transaction() { }
        public Transaction(Guid userId,
                           Guid pocketId,
                           BalanceSource impactedBalance,
                           decimal amount,
                           bool isIncome,
                           DateTime transactionDate,
                           bool isPrimaryIncome,
                           Guid? categoryId = null,
                           string? description = null)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Amount must be positive.", nameof(amount));
            }

            Id = Guid.NewGuid();
            UserId = userId;
            PocketId = pocketId;
            ImpactedBalance = impactedBalance;
            Amount = amount;
            IsIncome = isIncome;
            TransactionDate = transactionDate;
            IsPrimaryIncome = isPrimaryIncome;
            CategoryId = categoryId;
            Description = description;
        }

        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Guid PocketId { get; private set; }
        public Guid? CategoryId { get; private set; }
        public Guid? OriginalTransactionId { get; private set; }

        public BalanceSource ImpactedBalance { get; private set; } // BankLiquidity - CashLiquidity - BankSavings - CashSavings 
        public bool IsPrimaryIncome { get; private set; }
        public decimal Amount { get; private set; } // sempre positivo
        public bool IsIncome { get; set; } // true = entrata, false = uscita
        public decimal SignedAmount => IsIncome ? Amount : -Amount;

        public string? Description { get; set; }
        public DateTime TransactionDate { get; set; }

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
        public Category? Category { get; set; }
        public Transaction? OriginalTransaction { get; set; }
        public ICollection<Transaction> Refunds { get; set; } = new List<Transaction>();

        // DOMAIN METHODS
        public Transaction CreateRefund(decimal refundAmount,
                                        DateTime refundDate,
                                        string? refundDescription = null,
                                        BalanceSource? refundBalanceSourceOverride = null)
        {
            if (refundAmount <= 0)
            {
                throw new ArgumentException("Refund amount must be positive.", nameof(refundAmount));
            }

            var isIncomeForRefund = !IsIncome;

            var refund = new Transaction(UserId,
                                         PocketId,
                                         refundBalanceSourceOverride ?? ImpactedBalance,
                                         refundAmount,
                                         isIncomeForRefund,
                                         refundDate,
                                         false,
                                         CategoryId,
                                         refundDescription)
            {
                OriginalTransactionId = Id // this.Id
            };

            return refund;
        }

        public void ChangeCategory(Guid? categoryId) => CategoryId = categoryId;
        public void ChangeDescription(string? description) => Description = description;
        public void ChangeImpactedBalance(BalanceSource balanceSource) => ImpactedBalance = balanceSource;
        public void ChangeAmount(decimal amount) => Amount = amount;
        public void MarkAsPrimaryIncome() => IsPrimaryIncome = true;
        public void UnmarkPrimaryIncome() => IsPrimaryIncome = false;
    }
}
