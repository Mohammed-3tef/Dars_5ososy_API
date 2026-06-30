using Dars_5ososy_API.Application.DTOs;
using Dars_5ososy_API.Application.Services;
using Dars_5ososy_API.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dars_5ososy_API.Controllers
{
    [Route("api/education-stages")]
    [ApiController]
    public class EducationStagesController : ControllerBase
    {
        private readonly EducationStageService _educationStageService;

        public EducationStagesController(EducationStageService educationStageService)
        {
            _educationStageService = educationStageService;
        }

        /// <summary>Get all education stages.</summary>
        /// <response code="200">Education stages retrieved successfully.</response>
        /// <response code="404">No education stages found.</response>
        [HttpGet("get-all")]
        [ProducesResponseType(typeof(ApiResponse<List<EducationStageDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllEducationStages()
        {
            var educationStages = await _educationStageService.GetAllAsync();
            if (educationStages == null || !educationStages.Any())
                return NotFound(ApiResponse<object>.Fail("No education stages found."));
            return Ok(ApiResponse<List<EducationStageDTO>>.Succeeded(educationStages, "Education stages retrieved successfully."));
        }

        /// <summary>Get an education stage by its name.</summary>
        /// <response code="200">Education stage retrieved successfully.</response>
        /// <response code="404">Education stage not found.</response>
        [HttpGet("get-by-name/{educationStageName}")]
        [ProducesResponseType(typeof(ApiResponse<EducationStageDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEducationStageByName(string educationStageName)
        {
            var educationStage = await _educationStageService.GetByNameAsync(educationStageName);
            if (educationStage == null)
                return NotFound(ApiResponse<object>.Fail("Education stage not found."));
            return Ok(ApiResponse<EducationStageDTO>.Succeeded(educationStage, "Education stage retrieved successfully."));
        }

        /// <summary>Create a new education stage.</summary>
        /// <remarks>Only users with the <c>Admin</c> role can create an education stage.</remarks>
        /// <response code="201">Education stage created successfully.</response>
        /// <response code="400">Education stage with the same name already exists.</response>
        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ApiResponse<EducationStageDTO>), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateEducationStage(EducationStageDTO educationStageDto)
        {
            var createdEducationStage = await _educationStageService.CreateAsync(educationStageDto);
            if (createdEducationStage == null)
                return BadRequest(ApiResponse<object>.Fail("Education stage with the same name already exists."));
            return CreatedAtAction(nameof(GetEducationStageByName), new { EducationStageName = createdEducationStage.EnglishName }, ApiResponse<EducationStageDTO>.Succeeded(createdEducationStage, "Education stage created successfully."));
        }

        /// <summary>Update an existing education stage.</summary>
        /// <remarks>Only users with the <c>Admin</c> role can update an education stage.</remarks>
        /// <response code="200">Education stage updated successfully.</response>
        /// <response code="404">Education stage not found.</response>
        [HttpPut("update")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ApiResponse<EducationStageDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateEducationStage(EducationStageDTO educationStageDto)
        {
            var updatedEducationStage = await _educationStageService.UpdateAsync(educationStageDto);
            if (updatedEducationStage == null)
                return NotFound(ApiResponse<object>.Fail("Education stage not found."));
            return Ok(ApiResponse<EducationStageDTO>.Succeeded(updatedEducationStage, "Education stage updated successfully."));
        }

        /// <summary>Delete an education stage.</summary>
        /// <remarks>Only users with the <c>Admin</c> role can delete an education stage.</remarks>
        /// <response code="200">Education stage deleted successfully.</response>
        /// <response code="404">Education stage not found.</response>
        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteEducationStage(long id)
        {
            var isDeleted = await _educationStageService.DeleteAsync(id);
            if (!isDeleted)
                return NotFound(ApiResponse<object>.Fail("Education stage not found."));
            return Ok(ApiResponse<object>.Succeeded(null, "Education stage deleted successfully."));
        }
    }
}
