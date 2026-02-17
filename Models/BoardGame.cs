using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BoardGameLibrary.Models
{
    public class BoardGame
    {
        public int Id { get; set; }

        [AllowNull]
        public string Barcode { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Publisher { get; set; }

        public int? YearPublished { get; set; }

        [Range(1, 100)]
        public int? MinPlayers { get; set; }

        [Range(1, 100)]
        public int? MaxPlayers { get; set; }

        [Range(1, 1000)]
        public int? PlayingTimeMinutes { get; set; }

        [StringLength(50)]
        public string? Complexity { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        public DateTime DateAdded { get; set; } = DateTime.UtcNow;

        // Optional: Track quantity if you have multiple copies
        public int Quantity { get; set; } = 1;
        
        public int AvailableQuantity { get; set; } = 1;

        // Navigation properties
        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }
}
