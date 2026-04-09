using Microsoft.AspNetCore.Identity;
using Wallet.Api.Domain.Interfaces;

namespace Wallet.Api.Domain.Entities
{
    public class AppUser : IdentityUser<Guid>, IAuditable, ISoftDeletable
    {
        protected AppUser() { }
        public AppUser(string firstName)
        {
            FirstName = firstName;
        }

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
