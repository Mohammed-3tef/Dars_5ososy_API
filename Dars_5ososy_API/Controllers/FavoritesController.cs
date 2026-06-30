using Dars_5ososy_API.Application.DTOs;
using Dars_5ososy_API.Application.Services;
using Dars_5ososy_API.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dars_5ososy_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly FavoriteService _FavoriteService;

        public FavoritesController(FavoriteService FavoriteService)
        {
            _FavoriteService = FavoriteService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllFavorites()
        {
            var Favorites = await _FavoriteService.GetAllAsync();
            if (Favorites == null || !Favorites.Any())
                return NotFound(ApiResponse<object>.Fail("No Favorites found."));
            return Ok(ApiResponse<List<FavoriteDTO>>.Succeeded(Favorites, "Favorites retrieved successfully."));
        }

        [HttpGet("get-by-student/{studentUsername}")]
        public async Task<IActionResult> GetFavoritesByStudentUsername(string studentUsername)
        {
            var Favorites = await _FavoriteService.GetByStudentUsernameAsync(studentUsername);
            if (Favorites == null || !Favorites.Any())
                return NotFound(ApiResponse<object>.Fail("No Favorites found for the specified student."));

            return Ok(ApiResponse<List<FavoriteDTO>>.Succeeded(Favorites, "Favorites retrieved successfully."));
        }

        [HttpGet("get-by-teacher/{teacherUsername}")]
        public async Task<IActionResult> GetFavoritesByTeacherUsername(string teacherUsername)
        {
            var Favorites = await _FavoriteService.GetByTeacherUsernameAsync(teacherUsername);
            if (Favorites == null || !Favorites.Any())
                return NotFound(ApiResponse<object>.Fail("No Favorites found for the specified teacher."));
            return Ok(ApiResponse<List<FavoriteDTO>>.Succeeded(Favorites, "Favorites retrieved successfully."));
        }

        [HttpGet("get/{studentUsername}/{teacherUsername}")]
        public async Task<IActionResult> GetFavoriteByStudentAndTeacher(string studentUsername, string teacherUsername)
        {
            var Favorite = await _FavoriteService.GetByStudentAndTeacherAsync(studentUsername, teacherUsername);
            if (Favorite == null)
                return NotFound(ApiResponse<object>.Fail("No Favorite found for the specified student and teacher."));

            return Ok(ApiResponse<FavoriteDTO>.Succeeded(Favorite, "Favorite retrieved successfully."));
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> CreateFavorite(FavoriteDTO FavoriteDto)
        {
            var createdFavorite = await _FavoriteService.CreateAsync(FavoriteDto);
            if (createdFavorite == null)
                return BadRequest(ApiResponse<object>.Fail("Favorite with the same details already exists."));

            return CreatedAtAction(nameof(GetAllFavorites), ApiResponse<FavoriteDTO>.Succeeded(createdFavorite, "Favorite created successfully."));
        }

        [HttpDelete("delete/{studentUsername}/{teacherUsername}")]
        [Authorize]
        public async Task<IActionResult> DeleteFavorite(string studentUsername, string teacherUsername)
        {
            var isDeleted = await _FavoriteService.DeleteAsync(studentUsername, teacherUsername);
            if (!isDeleted)
                return NotFound(ApiResponse<object>.Fail("Favorite not found."));

            return Ok(ApiResponse<object>.Succeeded(null, "Favorite deleted successfully."));
        }
    }
}
