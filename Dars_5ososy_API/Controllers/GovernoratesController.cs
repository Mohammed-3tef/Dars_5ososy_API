using Asp.Versioning;
using Dars_5ososy_API.Application.DTOs.AddressDTOs;
using Dars_5ososy_API.Application.Services;
using Dars_5ososy_API.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dars_5ososy_API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/governorates")]
    [ApiController]
    public class GovernoratesController : ControllerBase
    {
        private readonly GovernorateService _GovernorateService;

        public GovernoratesController(GovernorateService GovernorateService)
        {
            _GovernorateService = GovernorateService;
        }

        /// <summary>Get all governorates.</summary>
        /// <response code="200">Governorates retrieved successfully.</response>
        /// <response code="404">No governorates found.</response>
        [HttpGet("get-all")]
        [ProducesResponseType(typeof(ApiResponse<List<GovernorateDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllGovernorates()
        {
            var Governorates = await _GovernorateService.GetAllAsync();
            if (Governorates == null || !Governorates.Any())
                return NotFound(ApiResponse<object>.Failure("No Governorates found."));
            return Ok(ApiResponse<List<GovernorateDTO>>.Success(Governorates, "Governorates retrieved successfully."));
        }

        /// <summary>Get all governorates by province Arabic name.</summary>
        /// <response code="200">Governorates retrieved successfully.</response>
        /// <response code="404">No governorates found.</response>
        [HttpGet("get-all-by-province-arabic-name/{provinceArabicName}")]
        [ProducesResponseType(typeof(ApiResponse<List<GovernorateDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllGovernoratesByProvinceArabicName(string provinceArabicName)
        {
            var Governorates = await _GovernorateService.GetAllByProvinceArabicNameAsync(provinceArabicName);
            if (Governorates == null || !Governorates.Any())
                return NotFound(ApiResponse<object>.Failure("No Governorates found."));
            return Ok(ApiResponse<List<GovernorateDTO>>.Success(Governorates, "Governorates retrieved successfully."));
        }

        /// <summary>Get all governorates by province English name.</summary>
        /// <response code="200">Governorates retrieved successfully.</response>
        /// <response code="404">No governorates found.</response>
        [HttpGet("get-all-by-province-english-name/{provinceEnglishName}")]
        [ProducesResponseType(typeof(ApiResponse<List<GovernorateDTO>>), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetAllGovernoratesByProvinceEnglishName(string provinceEnglishName)
        {
            var Governorates = await _GovernorateService.GetAllByProvinceEnglishNameAsync(provinceEnglishName);
            if (Governorates == null || !Governorates.Any())
                return NotFound(ApiResponse<object>.Failure("No Governorates found."));
            return Ok(ApiResponse<List<GovernorateDTO>>.Success(Governorates, "Governorates retrieved successfully."));
        }

        /// <summary>Get a governorate by its Arabic name.</summary>
        /// <response code="200">Governorate retrieved successfully.</response>
        /// <response code="404">Governorate not found.</response>
        [HttpGet("get-by-arabic-name/{GovernorateArabicName}")]
        [ProducesResponseType(typeof(ApiResponse<GovernorateDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetGovernorateByArabicName(string GovernorateArabicName)
        {
            var Governorate = await _GovernorateService.GetByArabicNameAsync(GovernorateArabicName);
            if (Governorate == null)
                return NotFound(ApiResponse<object>.Failure("Governorate not found."));
            return Ok(ApiResponse<GovernorateDTO>.Success(Governorate, "Governorate retrieved successfully."));
        }

        /// <summary>Get a governorate by its English name.</summary>
        /// <response code="200">Governorate retrieved successfully.</response>
        /// <response code="404">Governorate not found.</response>
        [HttpGet("get-by-english-name/{GovernorateEnglishName}")]
        [ProducesResponseType(typeof(ApiResponse<GovernorateDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetGovernorateByEnglishName(string GovernorateEnglishName)
        {
            var Governorate = await _GovernorateService.GetByEnglishNameAsync(GovernorateEnglishName);
            if (Governorate == null)
                return NotFound(ApiResponse<object>.Failure("Governorate not found."));
            return Ok(ApiResponse<GovernorateDTO>.Success(Governorate, "Governorate retrieved successfully."));
        }

        /// <summary>Create a new governorate.</summary>
        /// <remarks>Only users with the <c>Admin</c> role can create a governorate.</remarks>
        /// <response code="201">Governorate created successfully.</response>
        /// <response code="400">Failureed to create governorate.</response>
        /// <response code="401">Unauthorized. User is not authenticated.</response>
        /// <response code="403">Forbidden. User does not have the required role.</response>
        /// <response code="409">Governorate with the same name already exists.</response>
        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ApiResponse<GovernorateDTO>), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateGovernorate(GovernorateDTO GovernorateDto)
        {
            var existingGovernorate = await _GovernorateService.GetByArabicNameAsync(GovernorateDto.ArabicName);
            if (existingGovernorate != null)
                return Conflict(ApiResponse<object>.Failure("Governorate with the same Arabic name already exists."));
            var createdGovernorate = await _GovernorateService.CreateAsync(GovernorateDto);
            if (createdGovernorate == null)
                return BadRequest(ApiResponse<object>.Failure("Failureed to create governorate."));
            return CreatedAtAction(nameof(GetGovernorateByArabicName), new { GovernorateArabicName = createdGovernorate.ArabicName }, ApiResponse<GovernorateDTO>.Success(createdGovernorate, "Governorate created successfully."));
        }

        /// <summary>Update a governorate.</summary>
        /// <remarks>Only users with the <c>Admin</c> role can update a governorate.</remarks>
        /// <response code="200">Governorate updated successfully.</response>
        /// <response code="400">Failureed to update governorate.</response>
        /// <response code="401">Unauthorized. User is not authenticated.</response>
        /// <response code="403">Forbidden. User does not have the required role.</response>
        /// <response code="404">Governorate not found.</response>
        [HttpPut("update")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ApiResponse<GovernorateDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateGovernorate(GovernorateDTO GovernorateDto)
        {
            var existingGovernorate = await _GovernorateService.GetByArabicNameAsync(GovernorateDto.ArabicName);
            if (existingGovernorate == null)
                return NotFound(ApiResponse<object>.Failure("Governorate not found."));
            var updatedGovernorate = await _GovernorateService.UpdateAsync(GovernorateDto);
            if (updatedGovernorate == null)
                return BadRequest(ApiResponse<object>.Failure("Failureed to update governorate."));
            return Ok(ApiResponse<GovernorateDTO>.Success(updatedGovernorate, "Governorate updated successfully."));
        }

        /// <summary>Delete a governorate by its Arabic name.</summary>
        /// <remarks>Only users with the <c>Admin</c> role can delete a governorate.</remarks>
        /// <response code="204">Governorate deleted successfully.</response>
        /// <response code="400">Failureed to delete governorate.</response>
        /// <response code="401">Unauthorized. User is not authenticated.</response>
        /// <response code="403">Forbidden. User does not have the required role.</response>
        /// <response code="404">Governorate not found.</response>
        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-by-arabic-name/{GovernorateArabicName}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteGovernorateByArabicName(string GovernorateArabicName)
        {
            var existingGovernorate = await _GovernorateService.GetByArabicNameAsync(GovernorateArabicName);
            if (existingGovernorate == null)
                return NotFound(ApiResponse<object>.Failure("Governorate not found."));
            var isDeleted = await _GovernorateService.DeleteByArabicNameAsync(GovernorateArabicName);
            if (!isDeleted)
                return BadRequest(ApiResponse<object>.Failure("Failureed to delete governorate."));
            return NoContent();
        }

        /// <summary>Delete a governorate by its English name.</summary>
        /// <remarks>Only users with the <c>Admin</c> role can delete a governorate.</remarks>
        /// <response code="204">Governorate deleted successfully.</response>
        /// <response code="400">Failureed to delete governorate.</response>
        /// <response code="401">Unauthorized. User is not authenticated.</response>
        /// <response code="403">Forbidden. User does not have the required role.</response>
        /// <response code="404">Governorate not found.</response>
        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-by-english-name/{GovernorateEnglishName}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteGovernorateByEnglishName(string GovernorateEnglishName)
        {
            var existingGovernorate = await _GovernorateService.GetByEnglishNameAsync(GovernorateEnglishName);
            if (existingGovernorate == null)
                return NotFound(ApiResponse<object>.Failure("Governorate not found."));
            var isDeleted = await _GovernorateService.DeleteByEnglishNameAsync(GovernorateEnglishName);
            if (!isDeleted)
                return BadRequest(ApiResponse<object>.Failure("Failureed to delete governorate."));
            return NoContent();
        }
    }
}
