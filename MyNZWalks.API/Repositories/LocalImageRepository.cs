using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MyNZWalks.API.Data;
using MyNZWalks.API.Models.Domain;

namespace MyNZWalks.API.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly NZWalkDBContext _dbContext;

        public LocalImageRepository(IWebHostEnvironment webHostEnvironment , 
            IHttpContextAccessor httpContextAccessor,
            NZWalkDBContext dBContext)
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dBContext;
        }
        public async Task<Image> Upload(Image image)
        {
            var localFilePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images",
                $"{image.FileName}{image.FileExtenstion}");

            //Upload image to local path
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            //https://localhost:1234/images/image.jpg
            var urlFilePath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtenstion}";
            image.FilePath = urlFilePath;


            // Add Image to the Images table
            await _dbContext.Images.AddAsync(image);
            await _dbContext.SaveChangesAsync();
            return image;

        }
    }
}
