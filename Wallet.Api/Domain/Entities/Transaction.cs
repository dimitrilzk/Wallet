using Wallet.Api.Domain.Enums;
using Wallet.Api.Domain.Interfaces;

namespace Wallet.Api.Domain.Entities
{
    public class Transaction : IEntity, IAuditable, ISoftDeletable
    {
        protected Transaction() { }
        public Transaction(Guid pocketId,
                           BalanceSource impactedBalance,
                           decimal amount,
                           bool isIncome,
                           DateTime transactionDate,
                           bool isPrimaryIncome,
                           Guid? categoryId = null,
                           string? description = null)
        {
            Id = Guid.NewGuid();
            PocketId = pocketId;
            ImpactedBalance = impactedBalance;
            ChangeAmount(amount);
            IsIncome = isIncome;
            TransactionDate = transactionDate;
            IsPrimaryIncome = isPrimaryIncome;
            CategoryId = categoryId;
            Description = description;
        }

        public Guid Id { get; private set; }
        public Guid PocketId { get; private set; }
        public Guid? CategoryId { get; private set; }
        public Guid? OriginalTransactionId { get; private set; }

        public bool IsPrimaryIncome { get; private set; }
        public bool IsIncome { get; private set; } // true = entrata, false = uscita
        public BalanceSource ImpactedBalance { get; private set; } // BankLiquidity - CashLiquidity - BankSavings - CashSavings 
        public decimal Amount { get; private set; } // sempre positivo
        public decimal SignedAmount => IsIncome ? Amount : -Amount;

        public string? Description { get; private set; }
        public DateTime TransactionDate { get; private set; }

        // IAuditable
        public DateTime CreatedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }

        // ISoftDeletable
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }

        // Navigation
        public Transaction? OriginalTransaction { get; set; }
        public ICollection<Transaction> Refunds { get; set; } = new List<Transaction>();

        // DOMAIN METHODS
        public Transaction CreateRefund(decimal refundAmount,
                                        DateTime refundDate,
                                        string? refundDescription = null,
                                        BalanceSource? refundBalanceSourceOverride = null)
        {
            var refund = new Transaction(PocketId,
                                         refundBalanceSourceOverride ?? ImpactedBalance,
                                         refundAmount,
                                         !IsIncome,//NB
                                         refundDate,
                                         false,
                                         CategoryId,
                                         refundDescription)
            {
                OriginalTransactionId = Id // this.Id
            };

            return refund;
        }

        public void ChangeTransactionDate(DateTime? transactionDate)
        {
            if (transactionDate == null)
            {
                throw new ArgumentNullException(nameof(transactionDate));
            }
            else
            {
                TransactionDate = transactionDate.Value;
            }
        }

        public void ChangeAmount(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Amount must be positive.", nameof(amount));
            }

            Amount = amount;
        }

        public void ChangeIsIncome(bool isIncome) => IsIncome = isIncome;
        public void ChangeCategory(Guid? categoryId) => CategoryId = categoryId;
        public void ChangeDescription(string? description) => Description = description;
        public void ChangeImpactedBalance(BalanceSource balanceSource) => ImpactedBalance = balanceSource;
        public void MarkAsPrimaryIncome() => IsPrimaryIncome = true;
        public void UnmarkPrimaryIncome() => IsPrimaryIncome = false;
    }
}
