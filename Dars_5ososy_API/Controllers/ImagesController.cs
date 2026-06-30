using Dars_5ososy_API.Application.Services;
using Dars_5ososy_API.Domain.Entities;
using Dars_5ososy_API.Shared.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Dars_5ososy_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImagesController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile image)
        {
            var entity = await _imageService.UploadImageAsync(image);
            if (entity == null)
                return BadRequest(ApiResponse<int>.Fail("Image upload failed."));
            return Ok(ApiResponse<int>.Succeeded(entity.Id, "Image uploaded successfully."));
        }

        [HttpGet("get-user/{username}")]
        public async Task<IActionResult> GetImagesByUser(string username)
        {
            var images = await _imageService.GetImagesByUserAsync(username);
            if (images == null || !images.Any())
                return NotFound(ApiResponse<List<Image>>.Fail("No images found for the specified user."));
            return Ok(ApiResponse<List<Image>>.Succeeded(images, "Images retrieved successfully."));
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetImage(int id)
        {
            var image = await _imageService.GetImageByIdAsync(id);
            if (image == null)
                return NotFound(ApiResponse<Image>.Fail("Image not found."));
            return File(image.Data, image.ContentType);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateImage(int id, IFormFile image)
        {
            var updatedImage = await _imageService.UpdateImage(id, image);
            if (updatedImage == null)
                return NotFound(ApiResponse<Image>.Fail("Image not found."));
            return Ok(ApiResponse<Image>.Succeeded(updatedImage, "Image updated successfully."));
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var result = await _imageService.DeleteImageAsync(id);
            if (!result)
                return NotFound(ApiResponse<object>.Fail("Image not found."));
            return Ok(ApiResponse<bool>.Succeeded(result, "Image deleted successfully."));
        }
    }
}
