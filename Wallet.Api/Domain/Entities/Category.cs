namespace Wallet.Api.Domain.Entities
{
    public class Category : BaseEntity
    {
        protected Category() : base(Guid.Empty) { }
        public Category(Guid createdByUserId, string name) : base(createdByUserId)
        {
            UserId = createdByUserId;
            Name = name;
        }

        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsSystemDefault { get; set; }

        public AppUser? User { get; set; }
        public Category? ParentCategory { get; set; }
        public ICollection<Category> Subcategories { get; set; } = new List<Category>();
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
