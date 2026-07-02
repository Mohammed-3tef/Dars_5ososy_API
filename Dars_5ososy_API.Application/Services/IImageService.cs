using Dars_5ososy_API.Application.DTOs.ImageDTOs;

namespace Dars_5ososy_API.Application.Services
{
    public interface IImageService
    {
        Task<ImageDTO?> GetImageByIdAsync(int id);
        Task<List<ImageDTO>> GetImagesByUserAsync(string username);
        Task<ImageDTO?> UploadImageAsync(UploadImageDTO imageDto);
        Task<ImageDTO?> UpdateImageAsync(int id, UpdateImageDTO imageDto);
        Task<bool> DeleteImageAsync(int id);
    }
}