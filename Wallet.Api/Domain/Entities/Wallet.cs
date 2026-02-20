namespace Wallet.Api.Domain.Entities
{
    public class Wallet : BaseEntity
    {
        public Wallet(Guid createdByUserId, int year) : base(createdByUserId)
        {
            UserId = createdByUserId;
            Year = year;
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int Year { get; set; }

        public virtual AppUser User { get; set; }
        public virtual ICollection<Pocket> Pockets { get; set; } = new List<Pocket>();
    }
}
