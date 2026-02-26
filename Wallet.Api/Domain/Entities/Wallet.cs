namespace Wallet.Api.Domain.Entities
{
    public class Wallet : BaseEntity
    {
        protected Wallet() : base(Guid.Empty) { }
        public Wallet(Guid createdByUserId, int year) : base(createdByUserId)
        {
            UserId = createdByUserId;
            Year = year;
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int Year { get; set; } = 0;

        public AppUser User { get; set; } = null!;
        public ICollection<Pocket> Pockets { get; set; } = new List<Pocket>();
    }
}
