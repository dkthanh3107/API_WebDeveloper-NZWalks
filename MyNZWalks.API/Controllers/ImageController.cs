using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNZWalks.API.Models.Domain;
using MyNZWalks.API.Models.DTOs;
using MyNZWalks.API.Repositories;

namespace MyNZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;

        public ImageController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageRequestDTO imageRequestDTO)
        {
            ValidateFileUpLoad(imageRequestDTO);
             if(ModelState.IsValid) 
            {
                //Convert DTO to domain model 
                var imageDomainModel = new Image
                {
                    File = imageRequestDTO.File,
                    FileExtenstion = Path.GetExtension(imageRequestDTO.File.FileName),
                    FileSizeBytes = imageRequestDTO.File.Length,
                    FileName = imageRequestDTO.FileName,
                    FileDescription = imageRequestDTO.FileDescription,
                };
                //use repository to upload image
                await _imageRepository.Upload(imageDomainModel);
                return Ok(imageDomainModel);
            }
            return BadRequest(ModelState);

        }
        private void ValidateFileUpLoad(ImageRequestDTO imageRequestDTO) 
        {
            var allowedExtenstion = new string[] { ".jpg", ".jped", ".png" };
            if(!allowedExtenstion.Contains(Path.GetExtension(imageRequestDTO.File.FileName))) 
            {
                ModelState.AddModelError("file", "Unsupport Extenstion File");
            }

            if(imageRequestDTO.File.Length > 10485760) 
            {
                ModelState.AddModelError("file", "File size more than 10mb , please upload a smaller size file.");
            }
        }
    }
}
