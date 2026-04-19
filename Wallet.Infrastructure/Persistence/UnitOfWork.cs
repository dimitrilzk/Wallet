using Microsoft.EntityFrameworkCore;
using Wallet.Application.Interfaces;
using Wallet.Domain.Interfaces;

namespace Wallet.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext dbContext;
        private readonly ICurrentUserService currentUserService;

        public UnitOfWork(AppDbContext dbContext, ICurrentUserService currentUserService)
        {
            this.dbContext = dbContext;
            this.currentUserService = currentUserService;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var userId = currentUserService.UserId;
            var entities = dbContext.ChangeTracker.Entries<IAuditable>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
                .ToList();

            foreach (var entity in entities)
            {
                var auditable = entity.Entity;

                if (entity.State == EntityState.Added)
                {
                    if (auditable.CreatedAt == default)
                    {
                        auditable.CreatedAt = DateTime.UtcNow;
                        auditable.UpdatedAt = auditable.CreatedAt;
                    }

                    if (auditable.CreatedBy is null && userId is not null)
                    {
                        auditable.CreatedBy = userId;
                        auditable.UpdatedBy = auditable.CreatedBy;
                    }
                }

                if (entity.State == EntityState.Modified)
                {
                    auditable.UpdatedAt = DateTime.UtcNow;

                    if (userId is not null)
                    {
                        auditable.UpdatedBy = userId;
                    }
                }
            }

            return await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
