using Wallet.Domain.Interfaces;

namespace Wallet.Domain.Entities
{
    public class User : IEntity, IAuditable, ISoftDeletable
    {
        protected User() { }
        public User(string firstName)
        {
            Id = Guid.NewGuid();
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
        }

        public Guid Id { get; private set; }

        public string FirstName { get; private set; }

        // IAuditable
        public DateTime CreatedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }

        // ISoftDeletable
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }

        public void ChangeFirstName(string firstName)
        {
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
        }
    }
}
