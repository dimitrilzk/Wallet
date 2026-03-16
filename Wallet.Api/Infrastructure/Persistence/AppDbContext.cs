using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;
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

                u.Property(u => u.BankLiquidity).HasColumnType("numeric(18,2)");
                u.Property(u => u.CashLiquidity).HasColumnType("numeric(18,2)");
                u.Property(u => u.BankSavings).HasColumnType("numeric(18,2)");
                u.Property(u => u.CashSavings).HasColumnType("numeric(18,2)");
                u.Property(u => u.InvestedCapital).HasColumnType("numeric(18,2)");
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

                // relazione 1 (User) : N (AnnualWallet)
                w.HasOne(w => w.User)
                  .WithMany(u => u.Wallets)
                  .HasForeignKey(w => w.UserId)
                  .OnDelete(DeleteBehavior.Cascade);// AppUser principal

                // Unique constraint (UserId, Year)
                w.HasIndex(w => new { w.UserId, w.Year })
                  .IsUnique();
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

                // relazione 1 (Wallet) : N (Pocket)
                p.HasOne(x => x.Wallet)
                 .WithMany(w => w.Pockets)
                 .HasForeignKey(x => x.WalletId)
                 .OnDelete(DeleteBehavior.Cascade);// Wallet principal

                // 1 (User) : N (Pocket)
                p.HasOne(p => p.User)
                 .WithMany(u => u.Pockets)
                 .HasForeignKey(p => p.UserId)
                 .OnDelete(DeleteBehavior.Cascade);// User principal
                // Name unico per wallet
                p.HasIndex(p => new { p.WalletId, p.Name })
                 .IsUnique();
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

                // 1 (Pocket) : N (Transaction)
                t.HasOne(t => t.Pocket)
                 .WithMany(p => p.Transactions)
                 .HasForeignKey(t => t.PocketId)
                 .OnDelete(DeleteBehavior.Cascade);// Pocket principal

                // 1 (Category) : N (Transaction) – opzionale
                t.HasOne(t => t.Category)
                 .WithMany(c => c.Transactions)
                 .HasForeignKey(t => t.CategoryId)
                 .OnDelete(DeleteBehavior.SetNull);// Category principal

                // self reference per rimborsi: 1 (Original) : N (Refunds)
                t.HasOne(t => t.OriginalTransaction)
                 .WithMany(o => o.Refunds)
                 .HasForeignKey(t => t.OriginalTransactionId)
                 .OnDelete(DeleteBehavior.Cascade);// OriginalTransaction principal
            });
            // Category
            builder.Entity<Category>(c =>
            {
                c.ToTable("Categories");

                c.HasKey(c => c.Id);

                c.Property(c => c.Name)
                 .IsRequired()
                 .HasMaxLength(100);

                // 1 (User) : N (Category)
                c.HasOne(c => c.User)
                 .WithMany(u => u.Categories)
                 .HasForeignKey(c => c.UserId)
                 .OnDelete(DeleteBehavior.Cascade);// AppUser principal

                // self reference 1 (Parent) : N (Subcategories)
                c.HasOne(c => c.ParentCategory)
                 .WithMany(c => c.Subcategories)
                 .HasForeignKey(c => c.ParentCategoryId)
                 .OnDelete(DeleteBehavior.Restrict); // Categories principal
                // Name unico per User
                c.HasIndex(c => new { c.UserId, c.Name })
                 .IsUnique();
            });
        }
    }
}
