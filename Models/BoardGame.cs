using System.ComponentModel.DataAnnotations;

namespace BoardGameLibrary.Models
{
    public class BoardGame
    {
        public int Id { get; set; }

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

        [Required]
        public string OwnerId { get; set; } = string.Empty;

        // Navigation properties
        public ApplicationUser? Owner { get; set; }
        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }
}
