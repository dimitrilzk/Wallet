using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Wallet.Api.Domain.Entities;

namespace Wallet.Api.Infrastructure.Persistence
{
    public class AppDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<AnnualWallet> Wallets { get; set; }
        public DbSet<Pocket> Pockets { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<UserFinancialState> UserFinancialStates { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); //prima chiamata a base cosi Identity configura le sue tabelle

            // AppUser
            builder.Entity<AppUser>(u =>
            {
                // AspNetUsers è già configurata dalla base class; si possono solo aggiungere constraints
                u.Property(u => u.FirstName)
                 .IsRequired()
                 .HasMaxLength(100);

                u.HasQueryFilter(u => u.IsDeleted == false);
            });
            // Wallet
            builder.Entity<AnnualWallet>(w =>
            {
                w.ToTable("Wallets");

                w.HasKey(w => w.Id);

                w.Property(w => w.Year)
                    .IsRequired();

                w.Property(w => w.WalletStatus)
                    .IsRequired();

                // Unique constraint (UserId, Year)
                w.HasIndex(w => new { w.UserId, w.Year })
                  .IsUnique();

                w.HasQueryFilter(w => w.IsDeleted == false);
            });
            // Pocket
            builder.Entity<Pocket>(p =>
            {
                p.ToTable("Pockets");

                p.HasKey(p => p.Id);

                p.Property(p => p.Name)
                 .IsRequired()
                 .HasMaxLength(100);

                p.Property(p => p.Role)
                 .IsRequired();

                p.Property(p => p.DefaultBalanceSource)
                 .IsRequired();

                p.HasQueryFilter(p => p.IsDeleted == false);
            });
            // Transaction
            builder.Entity<Transaction>(t =>
            {
                t.ToTable("Transactions");

                t.HasKey(t => t.Id);

                t.Property(t => t.Amount)
                 .IsRequired();

                t.Property(t => t.ImpactedBalance)
                 .IsRequired();

                t.Property(t => t.TransactionDate)
                 .IsRequired();

                // self reference per rimborsi: 1 (Original) : N (Refunds)
                t.HasOne(t => t.OriginalTransaction)
                 .WithMany(o => o.Refunds)
                 .HasForeignKey(t => t.OriginalTransactionId)
                 .OnDelete(DeleteBehavior.Cascade);// OriginalTransaction principal

                t.HasQueryFilter(t => t.IsDeleted == false);
            });
            // Category
            builder.Entity<Category>(c =>
            {
                c.ToTable("Categories");

                c.HasKey(c => c.Id);

                c.Property(c => c.Name)
                 .IsRequired()
                 .HasMaxLength(100);

                // self reference 1 (Parent) : N (Subcategories)
                c.HasOne(c => c.ParentCategory)
                 .WithMany(c => c.Subcategories)
                 .HasForeignKey(c => c.ParentCategoryId)
                 .OnDelete(DeleteBehavior.Restrict); // Categories principal
                // Name unico per User
                c.HasIndex(c => new { c.UserId, c.Name })
                 .IsUnique();

                c.HasQueryFilter(c => c.IsDeleted == false);
            });
            // UserFinancialState
            builder.Entity<UserFinancialState>(u =>
            {
                u.ToTable("UserFinancialStates");

                u.HasKey(u => u.Id);

                u.Property(u => u.BankLiquidity).HasColumnType("numeric(18,2)");
                u.Property(u => u.CashLiquidity).HasColumnType("numeric(18,2)");
                u.Property(u => u.BankSavings).HasColumnType("numeric(18,2)");
                u.Property(u => u.CashSavings).HasColumnType("numeric(18,2)");
                u.Property(u => u.InvestedCapital).HasColumnType("numeric(18,2)");

                u.HasQueryFilter(u => u.IsDeleted == false);
            });
        }
    }
}
