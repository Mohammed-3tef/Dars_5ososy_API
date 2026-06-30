using Dars_5ososy_API.Application.DTOs;
using Dars_5ososy_API.Application.Services;
using Dars_5ososy_API.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dars_5ososy_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationStagesController : ControllerBase
    {
        private readonly EducationStageService _educationStageService;

        public EducationStagesController(EducationStageService educationStageService)
        {
            _educationStageService = educationStageService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllEducationStages()
        {
            var educationStages = await _educationStageService.GetAllAsync();
            if (educationStages == null || !educationStages.Any())
                return NotFound(ApiResponse<object>.Fail("No education stages found."));
            return Ok(ApiResponse<List<EducationStageDTO>>.Succeeded(educationStages, "Education stages retrieved successfully."));
        }

        [HttpGet("get-by-name/{educationStageName}")]
        public async Task<IActionResult> GetEducationStageByName(string educationStageName)
        {
            var educationStage = await _educationStageService.GetByNameAsync(educationStageName);

            if (educationStage == null)
                return NotFound(ApiResponse<object>.Fail("Education stage not found."));

            return Ok(ApiResponse<EducationStageDTO>.Succeeded(educationStage, "Education stage retrieved successfully."));
        }

        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateEducationStage(EducationStageDTO educationStageDto)
        {
            var createdEducationStage = await _educationStageService.CreateAsync(educationStageDto);
            if (createdEducationStage == null)
                return BadRequest(ApiResponse<object>.Fail("Education stage with the same name already exists."));

            return CreatedAtAction(nameof(GetEducationStageByName), new { EducationStageName = createdEducationStage.EnglishName }, ApiResponse<EducationStageDTO>.Succeeded(createdEducationStage, "Education stage created successfully."));
        }

        [HttpPut("update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateEducationStage(EducationStageDTO educationStageDto)
        {
            var updatedEducationStage = await _educationStageService.UpdateAsync(educationStageDto);
            if (updatedEducationStage == null)
                return NotFound(ApiResponse<object>.Fail("Education stage not found."));

            return Ok(ApiResponse<EducationStageDTO>.Succeeded(updatedEducationStage, "Education stage updated successfully."));
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEducationStage(long id)
        {
            var isDeleted = await _educationStageService.DeleteAsync(id);
            if (!isDeleted)
                return NotFound(ApiResponse<object>.Fail("Education stage not found."));

            return Ok(ApiResponse<object>.Succeeded(null, "Education stage deleted successfully."));
        }
    }
}
