using Dars_5ososy_API.Domain.Entities;

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
}