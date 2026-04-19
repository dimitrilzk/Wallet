using Wallet.Domain.Interfaces;

namespace Wallet.Domain.Entities
{
    public class UserFinancialState : IEntity, IAuditable, ISoftDeletable
    {
        protected UserFinancialState() { }
        public UserFinancialState(Guid userId,
                           decimal bankLiquidity = 0m,
                           decimal cashLiquidity = 0m,
                           decimal bankSavings = 0m,
                           decimal cashSavings = 0m,
                           decimal investedCapital = 0m)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            BankLiquidity = bankLiquidity;
            CashLiquidity = cashLiquidity;
            BankSavings = bankSavings;
            CashSavings = cashSavings;
            InvestedCapital = investedCapital;
        }
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }

        public decimal BankLiquidity { get; private set; }
        public decimal CashLiquidity { get; private set; }
        public decimal BankSavings { get; private set; }
        public decimal CashSavings { get; private set; }
        public decimal InvestedCapital { get; private set; }

        // IAuditable
        public DateTime CreatedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }

        // ISoftDeletable
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }

        public void AdjustBankLiquidityBy(decimal delta) => BankLiquidity += delta; //delta => transaction.SignedAmount
        public void AdjustCashLiquidityBy(decimal delta) => CashLiquidity += delta;
        public void AdjustBankSavingsBy(decimal delta) => BankSavings += delta;
        public void AdjustCashSavingsBy(decimal delta) => CashSavings += delta;
        public void AdjustInvestedCapital(decimal delta) => InvestedCapital += delta;
    }
}
