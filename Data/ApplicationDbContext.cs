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

            builder.Entity<Loan>()
                .HasOne(l => l.CheckedOutBy)
                .WithMany(u => u.LoansCheckedOut)
                .HasForeignKey(l => l.CheckedOutById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Loan>()
                .HasOne(l => l.CheckedInBy)
                .WithMany(u => u.LoansCheckedIn)
                .HasForeignKey(l => l.CheckedInById)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
