using EG.Walks.Application.Interfaces;
using EG.Walks.Domain.Entities;
using EG.Walks.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace EG.Walks.Infrastructure.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly EGWalksDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ImageRepository(EGWalksDbContext dbContext, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }
        // This method is responsible for uploading an image to the server and saving its details in the database.
        public async Task<Image> ImageUploadAsync(Image image)
        {
            var localFilePath = Path.Combine(_webHostEnvironment.ContentRootPath, "images",
                $"{image.FileName}{image.FilePath}");

            // Upload the image file to local storage
            using var stream = new FileStream(localFilePath, FileMode.Create);

            await image.File.CopyToAsync(stream);

            var urlFilePath = $"{_httpContextAccessor.HttpContext.Request.Scheme}" +
                $"://{_httpContextAccessor.HttpContext.Request.Host}" +
                $"{_httpContextAccessor.HttpContext.Request.PathBase}" +
                $"/Images/{image.FileName}{image.FileExtention}";

            // Set the file path and URL in the image object
            image.FilePath = urlFilePath;

            // Save the image details in the database
            await _dbContext.Images.AddAsync(image);

            // Return the saved image object
            return image;
        }
    }
}
