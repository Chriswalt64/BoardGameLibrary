using System.ComponentModel.DataAnnotations;

namespace BoardGameLibrary.Models
{
    public class CreateGameViewModel
    {
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
    }
}