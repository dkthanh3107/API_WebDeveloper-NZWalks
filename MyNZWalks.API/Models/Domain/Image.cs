using System.ComponentModel.DataAnnotations.Schema;

namespace MyNZWalks.API.Models.Domain
{
    public class Image
    {
        public Guid Id { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }
        public string FileName { get; set; }
        public string? FileDescription { get; set; }
        //Mo rong , VD : image.jpg thi JPG se la FileExtenstion
        public string FileExtenstion { get; set; }
        public long FileSizeBytes { get; set; }
        public string FilePath { get; set; }
    }
}
