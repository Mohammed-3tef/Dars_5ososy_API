using Dars_5ososy_API.Application.DTOs;
using Dars_5ososy_API.Application.Services;
using Dars_5ososy_API.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dars_5ososy_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationSystemsController : ControllerBase
    {
        private readonly EducationSystemService _educationSystemService;

        public EducationSystemsController(EducationSystemService educationSystemService)
        {
            _educationSystemService = educationSystemService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllSubjects()
        {
            var educationSystems = await _educationSystemService.GetAllAsync();
            if (educationSystems == null) 
                return NotFound(ApiResponse<object>.Fail("No education systems found."));
            return Ok(ApiResponse<object>.Succeeded(educationSystems, "Education systems retrieved successfully."));
        }

        [HttpGet("get-by-name/{educationSystemName}")]
        public async Task<IActionResult> GetEducationSystemByName(string educationSystemName)
        {
            var educationSystem = await _educationSystemService.GetByNameAsync(educationSystemName);

            if (educationSystem == null)
                return NotFound(ApiResponse<object>.Fail("Education system not found."));

            return Ok(ApiResponse<object>.Succeeded(educationSystem, "Education system retrieved successfully."));
        }

        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateEducationSystem(EducationSystemDTO educationSystemDto)
        {
            var createdEducationSystem = await _educationSystemService.CreateAsync(educationSystemDto);
            if (createdEducationSystem == null)
                return BadRequest(ApiResponse<object>.Fail("Education system with the same name already exists."));

            return CreatedAtAction(nameof(GetEducationSystemByName), new { educationSystemName = createdEducationSystem.EnglishName }, ApiResponse<object>.Succeeded(createdEducationSystem, "Education system created successfully."));
        }

        [HttpPut("update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateEducationSystem(EducationSystemDTO educationSystemDto)
        {
            var updatedEducationSystem = await _educationSystemService.UpdateAsync(educationSystemDto);
            if (updatedEducationSystem == null)
                return NotFound(ApiResponse<object>.Fail("Education system not found."));

            return Ok(ApiResponse<object>.Succeeded(updatedEducationSystem, "Education system updated successfully."));
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEducationSystem(long id)
        {
            var isDeleted = await _educationSystemService.DeleteAsync(id);
            if (!isDeleted)
                return NotFound(ApiResponse<object>.Fail("Education system not found."));

            return Ok(ApiResponse<object>.Succeeded(null, "Education system deleted successfully."));
        }
    }
}
