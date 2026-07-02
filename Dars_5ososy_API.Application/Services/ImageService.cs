using AutoMapper;
using Dars_5ososy_API.Application.DTOs.ImageDTOs;
using Dars_5ososy_API.Domain.Entities;
using Dars_5ososy_API.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;

namespace Dars_5ososy_API.Application.Services
{
    public class ImageService : IImageService
    {
        private readonly IMapper _mapper;
        private readonly IImageRepository _imageRepository;
        private readonly string[] _allowedExtensions =
        {
            ".jpg", ".jpeg", ".png", ".gif", ".webp"
        };

        public ImageService(IImageRepository imageRepository, IMapper mapper)
        {
            _imageRepository = imageRepository;
            _mapper = mapper;
        }

        public async Task<ImageDTO?> GetImageByIdAsync(int id)
        {
            var image = await _imageRepository.GetByIdAsync(id);
            if (image == null)
                return null;
            return _mapper.Map<ImageDTO>(image);
        }

        public async Task<List<ImageDTO>> GetImagesByUserAsync(string username)
        {
            var images = await _imageRepository.GetByUserUsernameAsync(username);
            return _mapper.Map<List<ImageDTO>>(images);
        }

        public async Task<ImageDTO?> UploadImageAsync(UploadImageDTO imageDto)
        {
            if (imageDto == null || imageDto.Image == null || imageDto.Image.Length == 0)
                return null;

            var extension = Path.GetExtension(imageDto.Image.FileName).ToLowerInvariant();

            if (!_allowedExtensions.Contains(extension))
                return null;

            using var memoryStream = new MemoryStream();

            await imageDto.Image.CopyToAsync(memoryStream);

            var entity = new Image
            {
                Data = memoryStream.ToArray(),
                FileName = imageDto.Image.FileName,
                ContentType = imageDto.Image.ContentType,
                FileSize = imageDto.Image.Length,
                CreatedAt = DateTime.UtcNow
            };

            var createdImage = await _imageRepository.CreateAsync(entity, imageDto.Username);
            if (createdImage == null)
                return null;

            return _mapper.Map<ImageDTO>(createdImage);
        }

        public async Task<ImageDTO?> UpdateImageAsync(int id, UpdateImageDTO imageDto)
        {
            var existing = await _imageRepository.GetByIdAsync(id);

            if (existing == null)
                return null;

            if (imageDto == null || imageDto.Image == null || imageDto.Image.Length == 0)
                return null;

            var extension = Path.GetExtension(imageDto.Image.FileName).ToLowerInvariant();

            if (!_allowedExtensions.Contains(extension))
                return null;

            using var memoryStream = new MemoryStream();

            await imageDto.Image.CopyToAsync(memoryStream);

            existing.Data = memoryStream.ToArray();
            existing.FileName = imageDto.Image.FileName;
            existing.ContentType = imageDto.Image.ContentType;
            existing.FileSize = imageDto.Image.Length;
            existing.CreatedAt = DateTime.UtcNow;

            var updatedImage = await _imageRepository.UpdateAsync(existing);
            if (updatedImage == null)
                return null;

            return _mapper.Map<ImageDTO>(updatedImage);
        }

        public async Task<bool> DeleteImageAsync(int id)
        {
            return await _imageRepository.DeleteAsync(id);
        }
    }
}
