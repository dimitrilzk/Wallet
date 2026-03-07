using Wallet.Api.Domain.Interfaces;

namespace Wallet.Api.Domain.Entities
{
    public class Category : IEntity, IAuditable, ISoftDeletable
    {
        protected Category() { }
        public Category(Guid userId,
                        string name,
                        Guid? parentCategoryId = null)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            ParentCategoryId = parentCategoryId;
        }

        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Guid? ParentCategoryId { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string? Description { get; private set; }

        // IAuditable
        public DateTime CreatedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }

        // ISoftDeletable
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }

        // Navigation
        public AppUser? User { get; set; }
        public Category? ParentCategory { get; set; }
        public ICollection<Category> Subcategories { get; set; } = new List<Category>();
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

        // DOMAIN METHODS
        public void ChangeName(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public void ChangeParent(Category? newParent)
        {
            ParentCategoryId = newParent?.Id;
            ParentCategory = newParent;
        }
        public void ChangeDescription(string? description) => Description = description;
    }
}
