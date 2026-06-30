using Dars_5ososy_API.Application.DTOs.AddressDTOs;
using Dars_5ososy_API.Application.Services;
using Dars_5ososy_API.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dars_5ososy_API.Controllers
{
    [Route("api/[controller]")]
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
                return NotFound(ApiResponse<object>.Fail("No Governorates found."));
            return Ok(ApiResponse<List<GovernorateDTO>>.Succeeded(Governorates, "Governorates retrieved successfully."));
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
                return NotFound(ApiResponse<object>.Fail("No Governorates found."));
            return Ok(ApiResponse<List<GovernorateDTO>>.Succeeded(Governorates, "Governorates retrieved successfully."));
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
                return NotFound(ApiResponse<object>.Fail("No Governorates found."));
            return Ok(ApiResponse<List<GovernorateDTO>>.Succeeded(Governorates, "Governorates retrieved successfully."));
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
                return NotFound(ApiResponse<object>.Fail("Governorate not found."));
            return Ok(ApiResponse<GovernorateDTO>.Succeeded(Governorate, "Governorate retrieved successfully."));
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
                return NotFound(ApiResponse<object>.Fail("Governorate not found."));
            return Ok(ApiResponse<GovernorateDTO>.Succeeded(Governorate, "Governorate retrieved successfully."));
        }

        /// <summary>Create a new governorate.</summary>
        /// <remarks>Only users with the <c>Admin</c> role can create a governorate.</remarks>
        /// <response code="201">Governorate created successfully.</response>
        /// <response code="400">Governorate with the same name already exists.</response>
        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ApiResponse<GovernorateDTO>), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateGovernorate(GovernorateDTO GovernorateDto)
        {
            var createdGovernorate = await _GovernorateService.CreateAsync(GovernorateDto);
            if (createdGovernorate == null)
                return BadRequest(ApiResponse<object>.Fail("Governorate with the same name already exists."));
            return CreatedAtAction(nameof(GetGovernorateByArabicName), new { GovernorateArabicName = createdGovernorate.ArabicName }, ApiResponse<GovernorateDTO>.Succeeded(createdGovernorate, "Governorate created successfully."));
        }

        /// <summary>Update a governorate.</summary>
        /// <remarks>Only users with the <c>Admin</c> role can update a governorate.</remarks>
        /// <response code="200">Governorate updated successfully.</response>
        /// <response code="404">Governorate not found.</response>
        [HttpPut("update")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ApiResponse<GovernorateDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateGovernorate(GovernorateDTO GovernorateDto)
        {
            var updatedGovernorate = await _GovernorateService.UpdateAsync(GovernorateDto);
            if (updatedGovernorate == null)
                return NotFound(ApiResponse<object>.Fail("Governorate not found."));
            return Ok(ApiResponse<GovernorateDTO>.Succeeded(updatedGovernorate, "Governorate updated successfully."));
        }

        /// <summary>Delete a governorate by its Arabic name.</summary>
        /// <remarks>Only users with the <c>Admin</c> role can delete a governorate.</remarks>
        /// <response code="200">Governorate deleted successfully.</response>
        /// <response code="404">Governorate not found.</response>
        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-by-arabic-name/{GovernorateArabicName}")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteGovernorateByArabicName(string GovernorateArabicName)
        {
            var isDeleted = await _GovernorateService.DeleteByArabicNameAsync(GovernorateArabicName);
            if (!isDeleted)
                return NotFound(ApiResponse<object>.Fail("Governorate not found."));
            return Ok(ApiResponse<object>.Succeeded(null, "Governorate deleted successfully."));
        }

        /// <summary>Delete a governorate by its English name.</summary>
        /// <remarks>Only users with the <c>Admin</c> role can delete a governorate.</remarks>
        /// <response code="200">Governorate deleted successfully.</response>
        /// <response code="404">Governorate not found.</response>
        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-by-english-name/{GovernorateEnglishName}")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteGovernorateByEnglishName(string GovernorateEnglishName)
        {
            var isDeleted = await _GovernorateService.DeleteByEnglishNameAsync(GovernorateEnglishName);
            if (!isDeleted)
                return NotFound(ApiResponse<object>.Fail("Governorate not found."));
            return Ok(ApiResponse<object>.Succeeded(null, "Governorate deleted successfully."));
        }
    }
}
