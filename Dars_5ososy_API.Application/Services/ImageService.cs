using Dars_5ososy_API.Domain.Entities;
using Dars_5ososy_API.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Dars_5ososy_API.Application.Services
{
    public interface IImageService
    {
        Task<Image> GetImageByIdAsync(int id);
        Task<Image> UploadImageAsync(IFormFile image);
        Task<Image?> UpdateImage(int id, IFormFile image);
        Task<bool> DeleteImageAsync(int id);
        Task<List<Image>> GetImagesByUserAsync(string username);
    }

    public class ImageService : IImageService
    {
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _context;
        private readonly string[] _allowedExtensions =
        {
            ".jpg", ".jpeg", ".png", ".gif", ".webp"
        };

        public ImageService(UserManager<User> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<Image> GetImageByIdAsync(int id)
        {
            var image = await _context.Images.FindAsync(id);
            if (image == null)
                throw new Exception("Image not found.");
            return image;
        }

        public async Task<List<Image>> GetImagesByUserAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
                return new List<Image>();

            var images = await _context.Images
                .Where(i => i.UserId == user.Id)
                .ToListAsync();
            return images;
        }

        public async Task<Image> UploadImageAsync(IFormFile image)
        {
            if (image == null || image.Length == 0)
                throw new Exception("Invalid image.");

            var extension =
                Path.GetExtension(image.FileName).ToLowerInvariant();

            if (!_allowedExtensions.Contains(extension))
                throw new Exception("Unsupported image format.");

            using var memoryStream = new MemoryStream();

            await image.CopyToAsync(memoryStream);

            var entity = new Image
            {
                Data = memoryStream.ToArray(),
                FileName = image.FileName,
                ContentType = image.ContentType,
                FileSize = image.Length,
                CreatedAt = DateTime.UtcNow
            };

            _context.Images.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Image?> UpdateImage(int id, IFormFile image)
        {
            var existing = await GetImageByIdAsync(id);

            if (existing == null)
                return null;

            var updated = await UploadImageAsync(image);

            existing.Data = updated.Data;
            existing.FileName = updated.FileName;
            existing.ContentType = updated.ContentType;
            existing.FileSize = updated.FileSize;
            existing.CreatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteImageAsync(int id)
        {
            var image = await GetImageByIdAsync(id);
            if (image == null)
                return false;
            _context.Images.Remove(image);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
