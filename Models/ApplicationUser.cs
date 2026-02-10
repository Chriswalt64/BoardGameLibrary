using Microsoft.AspNetCore.Identity;

namespace BoardGameLibrary.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        
        public string FullName => $"{FirstName} {LastName}".Trim();

        // Navigation properties
        public ICollection<Loan> BorrowedGames { get; set; } = new List<Loan>();
        public ICollection<Loan> LoansCheckedOut { get; set; } = new List<Loan>();
        public ICollection<Loan> LoansCheckedIn { get; set; } = new List<Loan>();
    }
}
