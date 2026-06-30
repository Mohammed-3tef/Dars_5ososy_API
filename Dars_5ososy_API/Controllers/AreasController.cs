using Dars_5ososy_API.Application.DTOs.AddressDTOs;
using Dars_5ososy_API.Application.Services;
using Dars_5ososy_API.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dars_5ososy_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreasController : ControllerBase
    {
        private readonly AreaService _AreaService;

        public AreasController(AreaService AreaService)
        {
            _AreaService = AreaService;
        }

        /// <summary>Gets all Areas.</summary>
        /// <response code="200">Areas retrieved successfully.</response>
        /// <response code="404">No Areas found.</response>
        [HttpGet("get-all")]
        [ProducesResponseType(typeof(ApiResponse<List<AreaDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAreas()
        {
            var Areas = await _AreaService.GetAllAsync();
            if (Areas == null || !Areas.Any())
                return NotFound(ApiResponse<object>.Fail("No Areas found."));
            return Ok(ApiResponse<List<AreaDTO>>.Succeeded(Areas, "Areas retrieved successfully."));
        }

        /// <summary>Gets all Areas by Province Arabic Name.</summary>
        /// <param name="provinceArabicName"></param>
        /// <response code="200">Areas retrieved successfully.</response>
        /// <response code="404">No Areas found.</response>
        [HttpGet("get-all-by-province-arabic-name/{provinceArabicName}")]
        [ProducesResponseType(typeof(ApiResponse<List<AreaDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAreasByProvinceArabicName(string provinceArabicName)
        {
            var Areas = await _AreaService.GetAllByProvinceArabicNameAsync(provinceArabicName);
            if (Areas == null || !Areas.Any())
                return NotFound(ApiResponse<object>.Fail("No Areas found."));
            return Ok(ApiResponse<List<AreaDTO>>.Succeeded(Areas, "Areas retrieved successfully."));
        }

        /// <summary>Gets all Areas by Province English Name.</summary>
        /// <param name="provinceEnglishName"></param>
        /// <response code="200">Areas retrieved successfully.</response>
        /// <response code="404">No Areas found.</response>
        [HttpGet("get-all-by-province-english-name/{provinceEnglishName}")]
        [ProducesResponseType(typeof(ApiResponse<List<AreaDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAreasByProvinceEnglishName(string provinceEnglishName)
        {
            var Areas = await _AreaService.GetAllByProvinceEnglishNameAsync(provinceEnglishName);
            if (Areas == null || !Areas.Any())
                return NotFound(ApiResponse<object>.Fail("No Areas found."));
            return Ok(ApiResponse<List<AreaDTO>>.Succeeded(Areas, "Areas retrieved successfully."));
        }

        /// <summary>Gets all Areas by Governorate Arabic Name.</summary>
        /// <param name="governorateArabicName"></param>
        /// <response code="200">Areas retrieved successfully.</response>
        /// <response code="404">No Areas found.</response>
        [HttpGet("get-all-by-governorate-arabic-name/{governorateArabicName}")]
        [ProducesResponseType(typeof(ApiResponse<List<AreaDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAreasByGovernorateArabicName(string governorateArabicName)
        {
            var Areas = await _AreaService.GetAllByGovernorateArabicNameAsync(governorateArabicName);
            if (Areas == null || !Areas.Any())
                return NotFound(ApiResponse<object>.Fail("No Areas found."));
            return Ok(ApiResponse<List<AreaDTO>>.Succeeded(Areas, "Areas retrieved successfully."));
        }

        /// <summary>Gets all Areas by Governorate English Name.</summary>
        /// <param name="governorateEnglishName"></param>
        /// <response code="200">Areas retrieved successfully.</response>
        /// <response code="404">No Areas found.</response>
        [HttpGet("get-all-by-governorate-english-name/{governorateEnglishName}")]
        [ProducesResponseType(typeof(ApiResponse<List<AreaDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAreasByGovernorateEnglishName(string governorateEnglishName)
        {
            var Areas = await _AreaService.GetAllByGovernorateEnglishNameAsync(governorateEnglishName);
            if (Areas == null || !Areas.Any())
                return NotFound(ApiResponse<object>.Fail("No Areas found."));
            return Ok(ApiResponse<List<AreaDTO>>.Succeeded(Areas, "Areas retrieved successfully."));
        }
        
        /// <summary>Gets an Area by Arabic Name.</summary>
        /// <param name="AreaArabicName"></param>
        /// <response code="200">Area retrieved successfully.</response>
        /// <response code="404">Area not found.</response>
        [HttpGet("get-by-arabic-name/{AreaArabicName}")]
        [ProducesResponseType(typeof(ApiResponse<AreaDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAreaByArabicName(string AreaArabicName)
        {
            var Area = await _AreaService.GetByArabicNameAsync(AreaArabicName);

            if (Area == null)
                return NotFound(ApiResponse<object>.Fail("Area not found."));

            return Ok(ApiResponse<AreaDTO>.Succeeded(Area, "Area retrieved successfully."));
        }

        /// <summary>Gets an Area by English Name.</summary>
        /// <param name="AreaEnglishName"></param>
        /// <response code="200">Area retrieved successfully.</response>
        /// <response code="404">Area not found.</response>
        [HttpGet("get-by-english-name/{AreaEnglishName}")]
        [ProducesResponseType(typeof(ApiResponse<AreaDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAreaByEnglishName(string AreaEnglishName)
        {
            var Area = await _AreaService.GetByEnglishNameAsync(AreaEnglishName);

            if (Area == null)
                return NotFound(ApiResponse<object>.Fail("Area not found."));

            return Ok(ApiResponse<AreaDTO>.Succeeded(Area, "Area retrieved successfully."));
        }

        /// <summary>Creates a new Area.</summary>
        /// <remarks>Only users with the <c>Admin</c> role can create a new area.</remarks>
        /// <param name="AreaDto"></param>
        /// <response code="201">Area created successfully.</response>
        /// <response code="400">Area with the same name already exists.</response>
        /// <response code="401">Unauthorized. User is not authenticated.</response>
        /// <response code="403">Forbidden. User does not have the required role.</response>
        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ApiResponse<AreaDTO>), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateArea(AreaDTO AreaDto)
        {
            var createdArea = await _AreaService.CreateAsync(AreaDto);
            if (createdArea == null)
                return BadRequest(ApiResponse<object>.Fail("Area with the same name already exists."));
            return CreatedAtAction(nameof(GetAreaByArabicName), new { AreaArabicName = createdArea.ArabicName }, ApiResponse<AreaDTO>.Succeeded(createdArea, "Area created successfully."));
        }

        /// <summary>Updates an existing Area by its Arabic or English name.</summary>
        /// <remarks>Only users with the <c>Admin</c> role can update an Area.</remarks>
        /// <param name="AreaDto"></param>
        /// <response code="200">Area updated successfully.</response>
        /// <response code="401">Unauthorized. User is not authenticated.</response>
        /// <response code="404">Area not found.</response>
        /// <response code="403">Forbidden. User does not have the required role.</response>
        [HttpPut("update")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ApiResponse<AreaDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateArea(AreaDTO AreaDto)
        {
            var updatedArea = await _AreaService.UpdateAsync(AreaDto);
            if (updatedArea == null)
                return NotFound(ApiResponse<object>.Fail("Area not found."));

            return Ok(ApiResponse<AreaDTO>.Succeeded(updatedArea, "Area updated successfully."));
        }

        /// <summary>Deletes an Area by its Arabic name.</summary>
        /// <remarks>Only users with the <c>Admin</c> role can delete an Area.</remarks>
        /// <param name="AreaArabicName"></param>
        /// <response code="200">Area deleted successfully.</response>
        /// <response code="401">Unauthorized. User is not authenticated.</response>
        /// <response code="403">Forbidden. User does not have the required role.</response>
        /// <response code="404">Area not found.</response>
        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-by-arabic-name/{AreaArabicName}")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteAreaByArabicName(string AreaArabicName)
        {
            var isDeleted = await _AreaService.DeleteByArabicNameAsync(AreaArabicName);
            if (!isDeleted)
                return NotFound(ApiResponse<object>.Fail("Area not found."));
            return Ok(ApiResponse<object>.Succeeded(null, "Area deleted successfully."));
        }

        /// <summary>Deletes an Area by its English name.</summary>
        /// <remarks>Only users with the <c>Admin</c> role can delete an Area.</remarks>
        /// <param name="AreaEnglishName"></param>
        /// <response code="200">Area deleted successfully.</response>
        /// <response code="401">Unauthorized. User is not authenticated.</response>
        /// <response code="403">Forbidden. User does not have the required role.</response>
        /// <response code="404">Area not found.</response>
        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-by-english-name/{AreaEnglishName}")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteAreaByEnglishName(string AreaEnglishName)
        {
            var isDeleted = await _AreaService.DeleteByEnglishNameAsync(AreaEnglishName);
            if (!isDeleted)
                return NotFound(ApiResponse<object>.Fail("Area not found."));
            return Ok(ApiResponse<object>.Succeeded(null, "Area deleted successfully."));
        }
    }
}
