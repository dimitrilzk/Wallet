using Wallet.Api.Application.Interfaces;

namespace Wallet.Api.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext dbContext;
        private readonly ICurrentUserService currentUserService;

        public UnitOfWork(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entity in dbContext.ChangeTracker)
            {

            }
        }
    }
}
