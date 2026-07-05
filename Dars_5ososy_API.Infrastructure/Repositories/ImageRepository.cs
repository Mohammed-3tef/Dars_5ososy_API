using Dars_5ososy_API.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Dars_5ososy_API.Infrastructure.Repositories
{
    public interface IImageRepository
    {
        Task<Image?> GetByIdAsync(int id);
        Task<List<Image>> GetByUserUsernameAsync(string username);
        Task<Image?> CreateAsync(Image entity, string username);
        Task<Image?> UpdateAsync(Image entity);
        Task<bool> DeleteAsync(int id);
    }

    public class ImageRepository : IImageRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _appDbContext;

        public ImageRepository(UserManager<User> userManager, AppDbContext appDbContext)
        {
            _userManager = userManager;
            _appDbContext = appDbContext;
        }

        public async Task<Image?> GetByIdAsync(int id)
        {
            return await _appDbContext.Images
                .Include(i => i.User)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<List<Image>> GetByUserUsernameAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return new List<Image>();

            return await _appDbContext.Images
                .Include(i => i.User)
                .AsNoTracking()
                .Where(i => i.UserId == user.Id)
                .ToListAsync();
        }

        public async Task<Image?> CreateAsync(Image entity, string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return null;

            entity.UserId = user.Id;
            entity.User = user;

            await _appDbContext.Images.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Image?> UpdateAsync(Image entity)
        {
            var existingImage = await GetByIdAsync(entity.Id);
            if (existingImage == null)
                return null;

            existingImage.Data = entity.Data;
            existingImage.FileName = entity.FileName;
            existingImage.ContentType = entity.ContentType;
            existingImage.FileSize = entity.FileSize;
            existingImage.CreatedAt = entity.CreatedAt;

            _appDbContext.Images.Update(existingImage);
            await _appDbContext.SaveChangesAsync();
            return existingImage;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var image = await GetByIdAsync(id);
            if (image == null)
                return false;

            _appDbContext.Images.Remove(image);
            var result = await _appDbContext.SaveChangesAsync();
            return result > 0;
        }
    }
}