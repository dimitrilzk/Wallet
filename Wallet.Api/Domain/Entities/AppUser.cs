using Microsoft.AspNetCore.Identity;
using Wallet.Api.Domain.Interfaces;

namespace Wallet.Api.Domain.Entities
{
    public class AppUser : IdentityUser<Guid>, IAuditable, ISoftDeletable
    {
        protected AppUser() { }
        public AppUser(string firstName,
                       decimal bankLiquidity = 0m,
                       decimal cashLiquidity = 0m,
                       decimal bankSavings = 0m,
                       decimal cashSavings = 0m)
        {
            FirstName = firstName;
            BankLiquidity = bankLiquidity;
            CashLiquidity = cashLiquidity;
            BankSavings = bankSavings;
            CashSavings = cashSavings;
        }

        public string FirstName { get; private set; }

        // Users starter pack
        public decimal BankLiquidity { get; set; }
        public decimal CashLiquidity { get; set; }
        public decimal BankSavings { get; set; }
        public decimal CashSavings { get; set; }

        // IAuditable
        public DateTime CreatedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }

        // ISoftDeletable
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }

        // Navigation
        public ICollection<AnnualWallet> Wallets { get; set; } = new List<AnnualWallet>();
        public ICollection<Category> Categories { get; set; } = new List<Category>();

        public void ChangeFirstName(string firstName)
        {
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
        }

        public void AdjustBankLiquidityBy(decimal delta) => BankLiquidity += delta; //delta => transaction.SignedAmount
        public void AdjustCashLiquidityBy(decimal delta) => CashLiquidity += delta;
        public void AdjustBankSavingsBy(decimal delta) => BankSavings += delta;
        public void AdjustCashSavingsBy(decimal delta) => CashSavings += delta;
    }
}
