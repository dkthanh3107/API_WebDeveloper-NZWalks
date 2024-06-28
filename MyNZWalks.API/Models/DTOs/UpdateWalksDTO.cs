using System.ComponentModel.DataAnnotations;

namespace MyNZWalks.API.Models.DTOs
{
    public class UpdateWalksDTO
    {
        [Required]
        [MaxLength(100, ErrorMessage = " Ten chi duoc viet toi da 100 ky tu")]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Range(0, 50)]
        public double LengthInKM { get; set; }
        public string? WalkImageURL { get; set; }
        [Required]

        public Guid DifficultyId { get; set; }
        [Required]
        public Guid RegionId { get; set; }

    }
}
