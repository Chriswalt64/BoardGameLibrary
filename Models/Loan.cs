using System.ComponentModel.DataAnnotations;

namespace BoardGameLibrary.Models
{
    public class Loan
    {
        public int Id { get; set; }

        [Required]
        public int BoardGameId { get; set; }

        [Required]
        public string BorrowerId { get; set; } = string.Empty;

        // Track which librarian created the loan
        public string? CheckedOutById { get; set; }

        [Required]
        public DateTime LoanDate { get; set; } = DateTime.UtcNow;

        public DateTime? DueDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        // Track which librarian processed the return
        public string? CheckedInById { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }

        public bool IsReturned => ReturnDate.HasValue;

        // Navigation properties
        public BoardGame? BoardGame { get; set; }
        public ApplicationUser? Borrower { get; set; }
        public ApplicationUser? CheckedOutBy { get; set; }
        public ApplicationUser? CheckedInBy { get; set; }
    }
}
