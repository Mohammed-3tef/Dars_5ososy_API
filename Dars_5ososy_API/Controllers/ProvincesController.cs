using Asp.Versioning;
using Dars_5ososy_API.Application.DTOs.AddressDTOs;
using Dars_5ososy_API.Application.Services;
using Dars_5ososy_API.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dars_5ososy_API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/provinces")]
    [ApiController]
    public class ProvincesController : ControllerBase
    {
        private readonly ProvinceService _ProvinceService;

        public ProvincesController(ProvinceService ProvinceService)
        {
            _ProvinceService = ProvinceService;
        }

        /// <summary>Get all provinces.</summary>
        /// <response code="200">Provinces retrieved successfully.</response>
        /// <response code="404">No provinces found.</response>
        [HttpGet("get-all")]
        [ProducesResponseType(typeof(ApiResponse<List<ProvinceDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllProvinces()
        {
            var Provinces = await _ProvinceService.GetAllAsync();
            if (Provinces == null || !Provinces.Any())
                return NotFound(ApiResponse<object>.Fail("No provinces found."));
            return Ok(ApiResponse<List<ProvinceDTO>>.Successed(Provinces, "Provinces retrieved successfully."));
        }

        /// <summary>Get a province by its Arabic name.</summary>
        /// <response code="200">Province retrieved successfully.</response>
        /// <response code="404">Province not found.</response>
        [HttpGet("get-by-arabic-name/{ProvinceArabicName}")]
        [ProducesResponseType(typeof(ApiResponse<ProvinceDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProvinceByArabicName(string ProvinceArabicName)
        {
            var Province = await _ProvinceService.GetByArabicNameAsync(ProvinceArabicName);
            if (Province == null)
                return NotFound(ApiResponse<object>.Fail("Province not found."));
            return Ok(ApiResponse<ProvinceDTO>.Successed(Province, "Province retrieved successfully."));
        }

        /// <summary>Get a province by its English name.</summary>
        /// <response code="200">Province retrieved successfully.</response>
        /// <response code="404">Province not found.</response>
        [HttpGet("get-by-english-name/{ProvinceEnglishName}")]
        [ProducesResponseType(typeof(ApiResponse<ProvinceDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProvinceByEnglishName(string ProvinceEnglishName)
        {
            var Province = await _ProvinceService.GetByEnglishNameAsync(ProvinceEnglishName);
            if (Province == null)
                return NotFound(ApiResponse<object>.Fail("Province not found."));
            return Ok(ApiResponse<ProvinceDTO>.Successed(Province, "Province retrieved successfully."));
        }

        /// <summary>Create a new Province</summary>
        /// <remarks>Only users with the <c>Admin</c> role can create a new area.</remarks>
        /// <response code="201">Province created successfully.</response>
        /// <response code="400">Failed to create province.</response>
        /// <response code="401">Unauthorized. User is not authenticated.</response>
        /// <response code="403">Forbidden. User does not have the required role.</response>
        /// <response code="409">Province with the same name already exists.</response>
        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ApiResponse<ProvinceDTO>), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateProvince(ProvinceDTO ProvinceDto)
        {
            var existingProvince = await _ProvinceService.GetByArabicNameAsync(ProvinceDto.ArabicName);
            if (existingProvince != null)
                return Conflict(ApiResponse<object>.Fail("Province with the same name already exists."));
            var createdProvince = await _ProvinceService.CreateAsync(ProvinceDto);
            if (createdProvince == null)
                return BadRequest(ApiResponse<object>.Fail("Failed to create province."));
            return CreatedAtAction(nameof(GetProvinceByArabicName), new { ProvinceArabicName = createdProvince.ArabicName }, ApiResponse<ProvinceDTO>.Successed(createdProvince, "Province created successfully."));
        }

        /// <summary>Update an existing Province</summary>
        /// <remarks>Only users with the <c>Admin</c> role can create a new area.</remarks>
        /// <response code="200">Province updated successfully.</response>
        /// <response code="400">Failed to update province.</response>
        /// <response code="401">Unauthorized. User is not authenticated.</response>
        /// <response code="403">Forbidden. User does not have the required role.</response>
        /// <response code="404">Province not found.</response>
        [HttpPut("update")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ApiResponse<ProvinceDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateProvince(ProvinceDTO ProvinceDto)
        {
            var existingProvince = await _ProvinceService.GetByArabicNameAsync(ProvinceDto.ArabicName);
            if (existingProvince == null)
                return NotFound(ApiResponse<object>.Fail("Province not found."));
            var updatedProvince = await _ProvinceService.UpdateAsync(ProvinceDto);
            if (updatedProvince == null)
                return BadRequest(ApiResponse<object>.Fail("Failed to update province."));
            return Ok(ApiResponse<ProvinceDTO>.Successed(updatedProvince, "Province updated successfully."));
        }

        /// <summary>Delete a province by its Arabic name.</summary>
        /// <remarks>Only users with the <c>Admin</c> role can create a new area.</remarks>
        /// <response code="204">Province deleted successfully.</response>
        /// <response code="400">Failed to delete province.</response>
        /// <response code="401">Unauthorized. User is not authenticated.</response>
        /// <response code="403">Forbidden. User does not have the required role.</response>
        /// <response code="404">Province not found.</response>
        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-by-arabic-name/{provinceArabicName}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteProvinceByArabicName(string provinceArabicName)
        {
            var existingProvince = await _ProvinceService.GetByArabicNameAsync(provinceArabicName);
            if (existingProvince == null)
                return NotFound(ApiResponse<object>.Fail("Province not found."));
            var isDeleted = await _ProvinceService.DeleteByArabicNameAsync(provinceArabicName);
            if (!isDeleted)
                return BadRequest(ApiResponse<object>.Fail("Failed to delete province."));
            return NoContent();
        }

        /// <summary>Delete a province by its English name.</summary>
        /// <remarks>Only users with the <c>Admin</c> role can create a new area.</remarks>
        /// <response code="204">Province deleted successfully.</response>
        /// <response code="400">Failed to delete province.</response>
        /// <response code="401">Unauthorized. User is not authenticated.</response>
        /// <response code="403">Forbidden. User does not have the required role.</response>
        /// <response code="404">Province not found.</response>
        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-by-english-name/{provinceEnglishName}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteProvinceByEnglishName(string provinceEnglishName)
        {
            var existingProvince = await _ProvinceService.GetByEnglishNameAsync(provinceEnglishName);
            if (existingProvince == null)
                return NotFound(ApiResponse<object>.Fail("Province not found."));
            var isDeleted = await _ProvinceService.DeleteByEnglishNameAsync(provinceEnglishName);
            if (!isDeleted)
                return BadRequest(ApiResponse<object>.Fail("Failed to delete province."));
            return NoContent();
        }
    }
}
