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

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllAreas()
        {
            var Areas = await _AreaService.GetAllAsync();
            if (Areas == null || !Areas.Any())
                return NotFound(ApiResponse<object>.Fail("No Areas found."));
            return Ok(ApiResponse<object>.Succeeded(Areas, "Areas retrieved successfully."));
        }

        [HttpGet("get-all-by-province-arabic-name/{provinceArabicName}")]
        public async Task<IActionResult> GetAllAreasByProvinceArabicName(string provinceArabicName)
        {
            var Areas = await _AreaService.GetAllByProvinceArabicNameAsync(provinceArabicName);
            if (Areas == null || !Areas.Any())
                return NotFound(ApiResponse<object>.Fail("No Areas found."));
            return Ok(ApiResponse<object>.Succeeded(Areas, "Areas retrieved successfully."));
        }

        [HttpGet("get-all-by-province-english-name/{provinceEnglishName}")]
        public async Task<IActionResult> GetAllAreasByProvinceEnglishName(string provinceEnglishName)
        {
            var Areas = await _AreaService.GetAllByProvinceEnglishNameAsync(provinceEnglishName);
            if (Areas == null || !Areas.Any())
                return NotFound(ApiResponse<object>.Fail("No Areas found."));
            return Ok(ApiResponse<object>.Succeeded(Areas, "Areas retrieved successfully."));
        }

        [HttpGet("get-all-by-governorate-arabic-name/{governorateArabicName}")]
        public async Task<IActionResult> GetAllAreasByGovernorateArabicName(string governorateArabicName)
        {
            var Areas = await _AreaService.GetAllByGovernorateArabicNameAsync(governorateArabicName);
            if (Areas == null || !Areas.Any())
                return NotFound(ApiResponse<object>.Fail("No Areas found."));
            return Ok(ApiResponse<object>.Succeeded(Areas, "Areas retrieved successfully."));
        }

        [HttpGet("get-all-by-governorate-english-name/{governorateEnglishName}")]
        public async Task<IActionResult> GetAllAreasByGovernorateEnglishName(string governorateEnglishName)
        {
            var Areas = await _AreaService.GetAllByGovernorateEnglishNameAsync(governorateEnglishName);
            if (Areas == null || !Areas.Any())
                return NotFound(ApiResponse<object>.Fail("No Areas found."));
            return Ok(ApiResponse<object>.Succeeded(Areas, "Areas retrieved successfully."));
        }

        [HttpGet("get-by-arabic-name/{AreaArabicName}")]
        public async Task<IActionResult> GetAreaByArabicName(string AreaArabicName)
        {
            var Area = await _AreaService.GetByArabicNameAsync(AreaArabicName);

            if (Area == null)
                return NotFound(ApiResponse<object>.Fail("Area not found."));

            return Ok(ApiResponse<object>.Succeeded(Area, "Area retrieved successfully."));
        }
        
        [HttpGet("get-by-english-name/{AreaEnglishName}")]
        public async Task<IActionResult> GetAreaByEnglishName(string AreaEnglishName)
        {
            var Area = await _AreaService.GetByEnglishNameAsync(AreaEnglishName);

            if (Area == null)
                return NotFound(ApiResponse<object>.Fail("Area not found."));

            return Ok(ApiResponse<object>.Succeeded(Area, "Area retrieved successfully."));
        }

        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateArea(AreaDTO AreaDto)
        {
            var createdArea = await _AreaService.CreateAsync(AreaDto);
            if (createdArea == null)
                return BadRequest(ApiResponse<object>.Fail("Area with the same name already exists."));
            return CreatedAtAction(nameof(GetAreaByArabicName), new { AreaArabicName = createdArea.ArabicName }, ApiResponse<object>.Succeeded(createdArea, "Area created successfully."));
        }

        [HttpPut("update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateArea(AreaDTO AreaDto)
        {
            var updatedArea = await _AreaService.UpdateAsync(AreaDto);
            if (updatedArea == null)
                return NotFound(ApiResponse<object>.Fail("Area not found."));

            return Ok(ApiResponse<object>.Succeeded(updatedArea, "Area updated successfully."));
        }

        [HttpDelete("delete-by-arabic-name/{AreaArabicName}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAreaByArabicName(string AreaArabicName)
        {
            var isDeleted = await _AreaService.DeleteByArabicNameAsync(AreaArabicName);
            if (!isDeleted)
                return NotFound(ApiResponse<object>.Fail("Area not found."));
            return Ok(ApiResponse<object>.Succeeded(null, "Area deleted successfully."));
        }
        
        [HttpDelete("delete-by-english-name/{AreaEnglishName}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAreaByEnglishName(string AreaEnglishName)
        {
            var isDeleted = await _AreaService.DeleteByEnglishNameAsync(AreaEnglishName);
            if (!isDeleted)
                return NotFound(ApiResponse<object>.Fail("Area not found."));
            return Ok(ApiResponse<object>.Succeeded(null, "Area deleted successfully."));
        }
    }
}
