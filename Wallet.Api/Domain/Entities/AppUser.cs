namespace Wallet.Api.Domain.Entities
{
    public class AppUser : BaseEntity
    {
        public AppUser(Guid createdByUserId, string firstName) : base(createdByUserId)
        {
            FirstName = firstName;
        }

        public string FirstName { get; set; }

        // Global balances
        public decimal CashBalance { get; set; }
        public decimal SavingsBalance { get; set; }
        public decimal InvestmentBalance { get; set; }


        // Navigation
        public ICollection<Wallet> Wallets { get; set; } = new List<Wallet>();
        public ICollection<Category> Categories { get; set; } = new List<Category>();
    }
}
