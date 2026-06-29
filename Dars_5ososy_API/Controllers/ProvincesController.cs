using Dars_5ososy_API.Application.DTOs;
using Dars_5ososy_API.Application.DTOs.AddressDTOs;
using Dars_5ososy_API.Application.Services;
using Dars_5ososy_API.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dars_5ososy_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvincesController : ControllerBase
    {
        private readonly ProvinceService _ProvinceService;

        public ProvincesController(ProvinceService ProvinceService)
        {
            _ProvinceService = ProvinceService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllProvinces()
        {
            var Provinces = await _ProvinceService.GetAllAsync();
            if (Provinces == null || !Provinces.Any())
                return NotFound(ApiResponse<object>.Fail("No provinces found."));
            return Ok(ApiResponse<object>.Succeeded(Provinces, "Provinces retrieved successfully."));
        }

        [HttpGet("get-by-arabic-name/{ProvinceArabicName}")]
        public async Task<IActionResult> GetProvinceByArabicName(string ProvinceArabicName)
        {
            var Province = await _ProvinceService.GetByArabicNameAsync(ProvinceArabicName);

            if (Province == null)
                return NotFound(ApiResponse<object>.Fail("Province not found."));

            return Ok(ApiResponse<object>.Succeeded(Province, "Province retrieved successfully."));
        }
        
        [HttpGet("get-by-english-name/{ProvinceEnglishName}")]
        public async Task<IActionResult> GetProvinceByEnglishName(string ProvinceEnglishName)
        {
            var Province = await _ProvinceService.GetByEnglishNameAsync(ProvinceEnglishName);

            if (Province == null)
                return NotFound(ApiResponse<object>.Fail("Province not found."));

            return Ok(ApiResponse<object>.Succeeded(Province, "Province retrieved successfully."));
        }

        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateProvince(ProvinceDTO ProvinceDto)
        {
            var createdProvince = await _ProvinceService.CreateAsync(ProvinceDto);
            if (createdProvince == null)
                return BadRequest(ApiResponse<object>.Fail("Province with the same name already exists."));
            return CreatedAtAction(nameof(GetProvinceByArabicName), new { ProvinceArabicName = createdProvince.ArabicName }, ApiResponse<object>.Succeeded(createdProvince, "Province created successfully."));
        }

        [HttpPut("update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProvince(ProvinceDTO ProvinceDto)
        {
            var updatedProvince = await _ProvinceService.UpdateAsync(ProvinceDto);
            if (updatedProvince == null)
                return NotFound(ApiResponse<object>.Fail("Province not found."));

            return Ok(ApiResponse<object>.Succeeded(updatedProvince, "Province updated successfully."));
        }

        [HttpDelete("delete-by-arabic-name/{provinceArabicName}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProvinceByArabicName(string provinceArabicName)
        {
            var isDeleted = await _ProvinceService.DeleteByArabicNameAsync(provinceArabicName);
            if (!isDeleted)
                return NotFound(ApiResponse<object>.Fail("Province not found."));
            return Ok(ApiResponse<object>.Succeeded(null, "Province deleted successfully."));
        }
        
        [HttpDelete("delete-by-english-name/{provinceEnglishName}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProvinceByEnglishName(string provinceEnglishName)
        {
            var isDeleted = await _ProvinceService.DeleteByEnglishNameAsync(provinceEnglishName);
            if (!isDeleted)
                return NotFound(ApiResponse<object>.Fail("Province not found."));
            return Ok(ApiResponse<object>.Succeeded(null, "Province deleted successfully."));
        }
    }
}
