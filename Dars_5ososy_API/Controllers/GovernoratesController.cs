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

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllGovernorates()
        {
            var Governorates = await _GovernorateService.GetAllAsync();
            if (Governorates == null || !Governorates.Any())
                return NotFound(ApiResponse<object>.Fail("No Governorates found."));
            return Ok(ApiResponse<object>.Succeeded(Governorates, "Governorates retrieved successfully."));
        }
        
        [HttpGet("get-all-by-province-arabic-name/{provinceArabicName}")]
        public async Task<IActionResult> GetAllGovernoratesByProvinceArabicName(string provinceArabicName)
        {
            var Governorates = await _GovernorateService.GetAllByProvinceArabicNameAsync(provinceArabicName);
            if (Governorates == null || !Governorates.Any())
                return NotFound(ApiResponse<object>.Fail("No Governorates found."));
            return Ok(ApiResponse<object>.Succeeded(Governorates, "Governorates retrieved successfully."));
        }
        
        [HttpGet("get-all-by-province-english-name/{provinceEnglishName}")]
        public async Task<IActionResult> GetAllGovernoratesByProvinceEnglishName(string provinceEnglishName)
        {
            var Governorates = await _GovernorateService.GetAllByProvinceEnglishNameAsync(provinceEnglishName);
            if (Governorates == null || !Governorates.Any())
                return NotFound(ApiResponse<object>.Fail("No Governorates found."));
            return Ok(ApiResponse<object>.Succeeded(Governorates, "Governorates retrieved successfully."));
        }

        [HttpGet("get-by-arabic-name/{GovernorateArabicName}")]
        public async Task<IActionResult> GetGovernorateByArabicName(string GovernorateArabicName)
        {
            var Governorate = await _GovernorateService.GetByArabicNameAsync(GovernorateArabicName);

            if (Governorate == null)
                return NotFound(ApiResponse<object>.Fail("Governorate not found."));

            return Ok(ApiResponse<object>.Succeeded(Governorate, "Governorate retrieved successfully."));
        }
        
        [HttpGet("get-by-english-name/{GovernorateEnglishName}")]
        public async Task<IActionResult> GetGovernorateByEnglishName(string GovernorateEnglishName)
        {
            var Governorate = await _GovernorateService.GetByEnglishNameAsync(GovernorateEnglishName);

            if (Governorate == null)
                return NotFound(ApiResponse<object>.Fail("Governorate not found."));

            return Ok(ApiResponse<object>.Succeeded(Governorate, "Governorate retrieved successfully."));
        }

        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateGovernorate(GovernorateDTO GovernorateDto)
        {
            var createdGovernorate = await _GovernorateService.CreateAsync(GovernorateDto);
            if (createdGovernorate == null)
                return BadRequest(ApiResponse<object>.Fail("Governorate with the same name already exists."));
            return CreatedAtAction(nameof(GetGovernorateByArabicName), new { GovernorateArabicName = createdGovernorate.ArabicName }, ApiResponse<object>.Succeeded(createdGovernorate, "Governorate created successfully."));
        }

        [HttpPut("update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateGovernorate(GovernorateDTO GovernorateDto)
        {
            var updatedGovernorate = await _GovernorateService.UpdateAsync(GovernorateDto);
            if (updatedGovernorate == null)
                return NotFound(ApiResponse<object>.Fail("Governorate not found."));

            return Ok(ApiResponse<object>.Succeeded(updatedGovernorate, "Governorate updated successfully."));
        }

        [HttpDelete("delete-by-arabic-name/{GovernorateArabicName}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteGovernorateByArabicName(string GovernorateArabicName)
        {
            var isDeleted = await _GovernorateService.DeleteByArabicNameAsync(GovernorateArabicName);
            if (!isDeleted)
                return NotFound(ApiResponse<object>.Fail("Governorate not found."));
            return Ok(ApiResponse<object>.Succeeded(null, "Governorate deleted successfully."));
        }
        
        [HttpDelete("delete-by-english-name/{GovernorateEnglishName}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteGovernorateByEnglishName(string GovernorateEnglishName)
        {
            var isDeleted = await _GovernorateService.DeleteByEnglishNameAsync(GovernorateEnglishName);
            if (!isDeleted)
                return NotFound(ApiResponse<object>.Fail("Governorate not found."));
            return Ok(ApiResponse<object>.Succeeded(null, "Governorate deleted successfully."));
        }
    }
}
