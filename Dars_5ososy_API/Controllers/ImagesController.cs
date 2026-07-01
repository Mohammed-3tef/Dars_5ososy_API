using Asp.Versioning;
using Dars_5ososy_API.Application.Services;
using Dars_5ososy_API.Domain.Entities;
using Dars_5ososy_API.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dars_5ososy_API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/images")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImagesController(IImageService imageService)
        {
            _imageService = imageService;
        }

        /// <summary>Upload a new image.</summary>
        /// <response code="201">Image uploaded successfully.</response>
        /// <response code="400">Image upload failed.</response>
        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile image)
        {
            var entity = await _imageService.UploadImageAsync(image);
            if (entity == null)
                return BadRequest(ApiResponse<int>.Fail("Image upload failed."));
            return Ok(ApiResponse<int>.Succeeded(entity.Id, "Image uploaded successfully."));
        }

        /// <summary>Get images by user.</summary>
        /// <response code="200">Images retrieved successfully.</response>
        /// <response code="404">No images found for the specified user.</response>
        [HttpGet("get-user/{username}")]
        public async Task<IActionResult> GetImagesByUser(string username)
        {
            var images = await _imageService.GetImagesByUserAsync(username);
            if (images == null || !images.Any())
                return NotFound(ApiResponse<List<Image>>.Fail("No images found for the specified user."));
            return Ok(ApiResponse<List<Image>>.Succeeded(images, "Images retrieved successfully."));
        }

        /// <summary>Get an image by ID.</summary>
        /// <response code="200">Image retrieved successfully.</response>
        /// <response code="404">Image not found.</response>
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetImage(int id)
        {
            var image = await _imageService.GetImageByIdAsync(id);
            if (image == null)
                return NotFound(ApiResponse<Image>.Fail("Image not found."));
            return File(image.Data, image.ContentType);
        }

        /// <summary>Update an existing image.</summary>
        /// <remarks>Only <c>Authorized users</c> can update an image.</remarks>
        /// <response code="200">Image updated successfully.</response>
        /// <response code="404">Image not found.</response>
        [Authorize]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateImage(int id, IFormFile image)
        {
            var updatedImage = await _imageService.UpdateImage(id, image);
            if (updatedImage == null)
                return NotFound(ApiResponse<Image>.Fail("Image not found."));
            return Ok(ApiResponse<Image>.Succeeded(updatedImage, "Image updated successfully."));
        }

        /// <summary>Delete an existing image.</summary>
        /// <remarks>Only <c>Authorized users</c> can delete an image.</remarks>
        /// <response code="200">Image deleted successfully.</response>
        /// <response code="404">Image not found.</response>
        [Authorize]
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
