using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BoardGameLibrary.Models;

namespace BoardGameLibrary.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<BoardGame> BoardGames { get; set; }
        public DbSet<Loan> Loans { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure BoardGame relationships
            builder.Entity<BoardGame>()
                .HasOne(bg => bg.Owner)
                .WithMany(u => u.OwnedGames)
                .HasForeignKey(bg => bg.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Loan relationships
            builder.Entity<Loan>()
                .HasOne(l => l.BoardGame)
                .WithMany(bg => bg.Loans)
                .HasForeignKey(l => l.BoardGameId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Loan>()
                .HasOne(l => l.Borrower)
                .WithMany(u => u.BorrowedGames)
                .HasForeignKey(l => l.BorrowerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
