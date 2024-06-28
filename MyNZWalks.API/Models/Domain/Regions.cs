using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyNZWalks.API.Models.Domain
{
    public class Regions
    {
        [Key]
        public Guid Id { get; set; }
        // Các thuộc tính khác của khu vực
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        public string? RegionImageURL { get; set; }
    }
}
