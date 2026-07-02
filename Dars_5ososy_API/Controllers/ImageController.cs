using Asp.Versioning;
using Dars_5ososy_API.Application.DTOs.ImageDTOs;
using Dars_5ososy_API.Application.Services;
using Dars_5ososy_API.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dars_5ososy_API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/images")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        /// <summary>Upload a new image.</summary>
        /// <response code="201">Image uploaded successfully.</response>
        /// <response code="400">Image upload failed.</response>
        [HttpPost("upload")]
        [ProducesResponseType(typeof(ApiResponse<ImageDTO>), StatusCodes.Status201Created)]
        public async Task<IActionResult> Upload([FromForm] UploadImageDTO imageDto)
        {
            var createdImage = await _imageService.UploadImageAsync(imageDto);
            if (createdImage == null)
                return BadRequest(ApiResponse<object>.Fail("Image upload failed."));

            return CreatedAtAction(
                nameof(GetImage),
                new { id = createdImage.Id },
                ApiResponse<ImageDTO>.Successed(createdImage, "Image uploaded successfully.")
            );
        }

        /// <summary>Get images by user.</summary>
        /// <response code="200">Images retrieved successfully.</response>
        /// <response code="404">No images found for the specified user.</response>
        [HttpGet("get-user/{username}")]
        [ProducesResponseType(typeof(ApiResponse<List<ImageDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetImagesByUser(string username)
        {
            var images = await _imageService.GetImagesByUserAsync(username);
            if (images == null || !images.Any())
                return NotFound(ApiResponse<object>.Fail("No images found for the specified user."));

            return Ok(ApiResponse<List<ImageDTO>>.Successed(images, "Images retrieved successfully."));
        }

        /// <summary>Get an image by ID.</summary>
        /// <response code="200">Image retrieved successfully.</response>
        /// <response code="404">Image not found.</response>
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetImage(int id)
        {
            var image = await _imageService.GetImageByIdAsync(id);
            if (image == null)
                return NotFound(ApiResponse<object>.Fail("Image not found."));

            return File(image.Data, image.ContentType, image.FileName);
        }

        /// <summary>Update an existing image.</summary>
        /// <remarks>Only <c>Authorized users</c> can update an image.</remarks>
        /// <response code="200">Image updated successfully.</response>
        /// <response code="404">Image not found.</response>
        [Authorize]
        [HttpPut("update/{id}")]
        [ProducesResponseType(typeof(ApiResponse<ImageDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateImage(int id, [FromForm] UpdateImageDTO imageDto)
        {
            var updatedImage = await _imageService.UpdateImageAsync(id, imageDto);
            if (updatedImage == null)
                return NotFound(ApiResponse<object>.Fail("Image not found."));

            return Ok(ApiResponse<ImageDTO>.Successed(updatedImage, "Image updated successfully."));
        }

        /// <summary>Delete an existing image.</summary>
        /// <remarks>Only <c>Authorized users</c> can delete an image.</remarks>
        /// <response code="204">Image deleted successfully.</response>
        /// <response code="404">Image not found.</response>
        [Authorize]
        [HttpDelete("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var result = await _imageService.DeleteImageAsync(id);
            if (!result)
                return NotFound(ApiResponse<object>.Fail("Image not found."));

            return NoContent();
        }
    }
}