namespace Wallet.Api.Domain.Entities
{
    public class Transaction : BaseEntity
    {
        public Transaction(Guid createdByUserId) : base(createdByUserId)
        {
            UserId = createdByUserId;
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid PocketId { get; set; }
        public Guid CategoryId { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public DateTime TransacrionDate { get; set; }
        public string ImpactedPocket { get; set; } // Savings, Investments, Cash

        public Pocket Pocket { get; set; }
        public TransactionCategory Category { get; set; }
    }
}
