using Wallet.Api.Domain.Interfaces;

namespace Wallet.Api.Domain.Entities
{
    public class AppUser : IEntity, IAuditable, ISoftDeletable
    {
        protected AppUser() { }
        public AppUser(string firstName,
                       decimal bankLiquidity = 0m,
                       decimal cashLiquidity = 0m,
                       decimal bankSavings = 0m,
                       decimal cashSavings = 0m)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            BankLiquidity = bankLiquidity;
            CashLiquidity = cashLiquidity;
            BankSavings = bankSavings;
            CashSavings = cashSavings;
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }

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
        public ICollection<Wallet> Wallets { get; set; } = new List<Wallet>();
        public ICollection<Category> Categories { get; set; } = new List<Category>();
    }
}
