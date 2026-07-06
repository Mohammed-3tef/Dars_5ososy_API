using Asp.Versioning;
using Dars_5ososy_API.Application.DTOs;
using Dars_5ososy_API.Application.Services;
using Dars_5ososy_API.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dars_5ososy_API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/education-stages")]
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
                return NotFound(ApiResponse<object>.Failure("No education stages found."));
            return Ok(ApiResponse<List<EducationStageDTO>>.Success(educationStages, "Education stages retrieved successfully."));
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
                return NotFound(ApiResponse<object>.Failure("Education stage not found."));
            return Ok(ApiResponse<EducationStageDTO>.Success(educationStage, "Education stage retrieved successfully."));
        }

        /// <summary>Create a new education stage.</summary>
        /// <remarks>Only users with the <c>Admin</c> role can create an education stage.</remarks>
        /// <response code="201">Education stage created successfully.</response>
        /// <response code="400">Education stage with the same name already exists.</response>
        /// <response code="401">Unauthorized. User is not authenticated.</response>
        /// <response code="403">Forbidden. User does not have the required role.</response>
        /// <response code="409">Education stage with the same name already exists.</response>
        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ApiResponse<EducationStageDTO>), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateEducationStage(EducationStageDTO educationStageDto)
        {
            var existingEducationStage = await _educationStageService.GetByNameAsync(educationStageDto.EnglishName);
            if (existingEducationStage != null)
                return Conflict(ApiResponse<object>.Failure("Education stage with the same name already exists."));
            var createdEducationStage = await _educationStageService.CreateAsync(educationStageDto);
            if (createdEducationStage == null)
                return BadRequest(ApiResponse<object>.Failure("Education stage with the same name already exists."));
            return CreatedAtAction(nameof(GetEducationStageByName), new { EducationStageName = createdEducationStage.EnglishName }, ApiResponse<EducationStageDTO>.Success(createdEducationStage, "Education stage created successfully."));
        }

        /// <summary>Update an existing education stage.</summary>
        /// <remarks>Only users with the <c>Admin</c> role can update an education stage.</remarks>
        /// <response code="200">Education stage updated successfully.</response>
        /// <response code="401">Unauthorized. User is not authenticated.</response>
        /// <response code="403">Forbidden. User does not have the required role.</response>
        /// <response code="404">Education stage not found.</response>
        /// <response code="409">Education stage with the same name already exists.</response>
        [HttpPut("update")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ApiResponse<EducationStageDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateEducationStage(EducationStageDTO educationStageDto)
        {
            var existingEducationStage = await _educationStageService.GetByNameAsync(educationStageDto.EnglishName);
            if (existingEducationStage != null)
                return Conflict(ApiResponse<object>.Failure("Education stage with the same name already exists."));
            var updatedEducationStage = await _educationStageService.UpdateAsync(educationStageDto);
            if (updatedEducationStage == null)
                return NotFound(ApiResponse<object>.Failure("Education stage not found."));
            return Ok(ApiResponse<EducationStageDTO>.Success(updatedEducationStage, "Education stage updated successfully."));
        }

        /// <summary>Delete an education stage.</summary>
        /// <remarks>Only users with the <c>Admin</c> role can delete an education stage.</remarks>
        /// <response code="204">Education stage deleted successfully.</response>
        /// <response code="400">Failureed to delete education stage.</response>
        /// <response code="401">Unauthorized. User is not authenticated.</response>
        /// <response code="403">Forbidden. User does not have the required role.</response>
        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteEducationStage(long id)
        {
            var isDeleted = await _educationStageService.DeleteAsync(id);
            if (!isDeleted)
                return BadRequest(ApiResponse<object>.Failure("Failureed to delete education stage."));
            return NoContent();
        }
    }
}
