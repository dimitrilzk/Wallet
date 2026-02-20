namespace Wallet.Api.Domain.Entities
{
    public class TransactionCategory : BaseEntity
    {
        public TransactionCategory(Guid createdByUserId) : base(createdByUserId)
        {
            UserId = createdByUserId;
        }

        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? EmojiIcon { get; set; }
        public bool IsLocked { get; set; }

        public virtual AppUser User { get; set; }
        public virtual ICollection<Transaction> Transactions {  get; set; } = new List<Transaction>();
    }
}
