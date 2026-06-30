using Dars_5ososy_API.Application.DTOs;
using Dars_5ososy_API.Application.Services;
using Dars_5ososy_API.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dars_5ososy_API.Controllers
{
    [Route("api/education-systems")]
    [ApiController]
    public class EducationSystemsController : ControllerBase
    {
        private readonly EducationSystemService _educationSystemService;

        public EducationSystemsController(EducationSystemService educationSystemService)
        {
            _educationSystemService = educationSystemService;
        }

        /// <summary>Get all education systems.</summary>
        /// <response code="200">Education systems retrieved successfully.</response>
        /// <response code="404">No education systems found.</response>
        [HttpGet("get-all")]
        [ProducesResponseType(typeof(ApiResponse<List<EducationSystemDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllSubjects()
        {
            var educationSystems = await _educationSystemService.GetAllAsync();
            if (educationSystems == null) 
                return NotFound(ApiResponse<object>.Fail("No education systems found."));
            return Ok(ApiResponse<List<EducationSystemDTO>>.Succeeded(educationSystems, "Education systems retrieved successfully."));
        }

        /// <summary>Get an education system by its name.</summary>
        /// <response code="200">Education system retrieved successfully.</response>
        /// <response code="404">Education system not found.</response>
        [HttpGet("get-by-name/{educationSystemName}")]
        [ProducesResponseType(typeof(ApiResponse<EducationSystemDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEducationSystemByName(string educationSystemName)
        {
            var educationSystem = await _educationSystemService.GetByNameAsync(educationSystemName);
            if (educationSystem == null)
                return NotFound(ApiResponse<object>.Fail("Education system not found."));
            return Ok(ApiResponse<EducationSystemDTO>.Succeeded(educationSystem, "Education system retrieved successfully."));
        }

        /// <summary>Create a new education system.</summary>
        /// <remarks>Only users with the <c>Admin</c> role can create a new education system.</remarks>
        /// <response code="201">Education system created successfully.</response>
        /// <response code="400">Education system with the same name already exists.</response>
        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ApiResponse<EducationSystemDTO>), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateEducationSystem(EducationSystemDTO educationSystemDto)
        {
            var createdEducationSystem = await _educationSystemService.CreateAsync(educationSystemDto);
            if (createdEducationSystem == null)
                return BadRequest(ApiResponse<object>.Fail("Education system with the same name already exists."));
            return CreatedAtAction(nameof(GetEducationSystemByName), new { educationSystemName = createdEducationSystem.EnglishName }, ApiResponse<EducationSystemDTO>.Succeeded(createdEducationSystem, "Education system created successfully."));
        }

        /// <summary>Update an existing education system.</summary>
        /// <remarks>Only users with the <c>Admin</c> role can update an education system.</remarks>
        /// <response code="200">Education system updated successfully.</response>
        /// <response code="404">Education system not found.</response>
        [HttpPut("update")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ApiResponse<EducationSystemDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateEducationSystem(EducationSystemDTO educationSystemDto)
        {
            var updatedEducationSystem = await _educationSystemService.UpdateAsync(educationSystemDto);
            if (updatedEducationSystem == null)
                return NotFound(ApiResponse<object>.Fail("Education system not found."));

            return Ok(ApiResponse<EducationSystemDTO>.Succeeded(updatedEducationSystem, "Education system updated successfully."));
        }

        /// <summary>Delete an education system by its ID.</summary>
        /// <remarks>Only users with the <c>Admin</c> role can delete an education system.</remarks>
        /// <response code="200">Education system deleted successfully.</response>
        /// <response code="404">Education system not found.</response>
        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteEducationSystem(long id)
        {
            var isDeleted = await _educationSystemService.DeleteAsync(id);
            if (!isDeleted)
                return NotFound(ApiResponse<object>.Fail("Education system not found."));

            return Ok(ApiResponse<object>.Succeeded(null, "Education system deleted successfully."));
        }
    }
}
