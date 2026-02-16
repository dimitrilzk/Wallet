namespace Wallet.Api.Domain.Entities
{
    public abstract class BaseEntity
    {
        protected BaseEntity(Guid UserId)
        {
            CreatedAt = DateTime.UtcNow;
            CreatedBy = UserId;
        }

        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set;  }
        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
    }
}
