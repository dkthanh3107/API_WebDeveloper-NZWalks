using System.ComponentModel.DataAnnotations;

namespace NZWalks.UI.Models
{
    public class CreateRegionsViewModel
    {
        [Required]
        [MinLength(3, ErrorMessage = "Chi duoc viet 3 ky tu")]
        [MaxLength(3, ErrorMessage = "Khong duoc lon hon 3 ky tu")]
        public string Code { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Tên chi duoc viet toi da 100 ky tu")]
        public string Name { get; set; }
        public string? RegionImageURL { get; set; }
    }
}
