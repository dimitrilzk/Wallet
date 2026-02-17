namespace Wallet.Api.Domain.Entities
{
    public abstract class BaseEntity
    {
        protected BaseEntity(Guid createdByUserId)
        {
            CreatedAt = DateTime.UtcNow;
            CreatedBy = createdByUserId;
        }

        public DateTime CreatedAt { get; private set; }
        public Guid CreatedBy { get; private set;  }
        public DateTime? UpdatedAt { get; private set; }
        public Guid? UpdatedBy { get; private set; }

        public void SetUpdated(Guid updatedByUserId)
        {
            UpdatedAt = DateTime.UtcNow;
            UpdatedBy = updatedByUserId;
        }
    }
}
