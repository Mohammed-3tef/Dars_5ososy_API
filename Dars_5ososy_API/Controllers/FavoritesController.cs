using Asp.Versioning;
using Dars_5ososy_API.Application.DTOs;
using Dars_5ososy_API.Application.Services;
using Dars_5ososy_API.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dars_5ososy_API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/favorites")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly FavoriteService _FavoriteService;

        public FavoritesController(FavoriteService FavoriteService)
        {
            _FavoriteService = FavoriteService;
        }

        /// <summary>Get all favorites.</summary>
        /// <response code="200">Favorites retrieved successfully.</response>
        /// <response code="404">No favorites found.</response>
        [HttpGet("get-all")]
        [ProducesResponseType(typeof(ApiResponse<List<FavoriteDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllFavorites()
        {
            var Favorites = await _FavoriteService.GetAllAsync();
            if (Favorites == null || !Favorites.Any())
                return NotFound(ApiResponse<object>.Failure("No Favorites found."));
            return Ok(ApiResponse<List<FavoriteDTO>>.Success(Favorites, "Favorites retrieved successfully."));
        }

        /// <summary>Get favorites by student username.</summary>
        /// <response code="200">Favorites retrieved successfully.</response>
        /// <response code="404">No favorites found for the specified student.</response>
        [HttpGet("get-by-student/{studentUsername}")]
        [ProducesResponseType(typeof(ApiResponse<List<FavoriteDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFavoritesByStudentUsername(string studentUsername)
        {
            var Favorites = await _FavoriteService.GetByStudentUsernameAsync(studentUsername);
            if (Favorites == null || !Favorites.Any())
                return NotFound(ApiResponse<object>.Failure("No Favorites found for the specified student."));

            return Ok(ApiResponse<List<FavoriteDTO>>.Success(Favorites, "Favorites retrieved successfully."));
        }

        /// <summary>Get favorites by teacher username.</summary>
        /// <response code="200">Favorites retrieved successfully.</response>
        /// <response code="404">No favorites found for the specified teacher.</response>
        [HttpGet("get-by-teacher/{teacherUsername}")]
        [ProducesResponseType(typeof(ApiResponse<List<FavoriteDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFavoritesByTeacherUsername(string teacherUsername)
        {
            var Favorites = await _FavoriteService.GetByTeacherUsernameAsync(teacherUsername);
            if (Favorites == null || !Favorites.Any())
                return NotFound(ApiResponse<object>.Failure("No Favorites found for the specified teacher."));
            return Ok(ApiResponse<List<FavoriteDTO>>.Success(Favorites, "Favorites retrieved successfully."));
        }

        /// <summary>Get a favorite by student and teacher usernames.</summary>
        /// <response code="200">Favorite retrieved successfully.</response>
        /// <response code="404">No favorite found for the specified student and teacher.</response>
        [HttpGet("get/{studentUsername}/{teacherUsername}")]
        [ProducesResponseType(typeof(ApiResponse<FavoriteDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFavoriteByStudentAndTeacher(string studentUsername, string teacherUsername)
        {
            var Favorite = await _FavoriteService.GetByStudentAndTeacherAsync(studentUsername, teacherUsername);
            if (Favorite == null)
                return NotFound(ApiResponse<object>.Failure("No Favorite found for the specified student and teacher."));

            return Ok(ApiResponse<FavoriteDTO>.Success(Favorite, "Favorite retrieved successfully."));
        }

        /// <summary>Create a new favorite.</summary>
        /// <remarks>Only <c>Authorized users</c> can create a favorite.</remarks>
        /// <response code="201">Favorite created successfully.</response>
        /// <response code="400">Failureed to create favorite.</response>
        /// <response code="401">Unauthorized. User is not authenticated.</response>
        /// <response code="409">Favorite with the same details already exists.</response>
        [Authorize]
        [HttpPost("create")]
        [ProducesResponseType(typeof(ApiResponse<FavoriteDTO>), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateFavorite(FavoriteDTO FavoriteDto)
        {
            var existingFavorite = await _FavoriteService.GetByStudentAndTeacherAsync(FavoriteDto.StudentUsername, FavoriteDto.TeacherUsername);
            if (existingFavorite != null)
                return Conflict(ApiResponse<object>.Failure("Favorite with the same details already exists."));
            var createdFavorite = await _FavoriteService.CreateAsync(FavoriteDto);
            if (createdFavorite == null)
                return BadRequest(ApiResponse<object>.Failure("Failureed to create favorite."));
            return CreatedAtAction(nameof(GetAllFavorites), ApiResponse<FavoriteDTO>.Success(createdFavorite, "Favorite created successfully."));
        }

        /// <summary>Delete a favorite by student and teacher usernames.</summary>
        /// <remarks>Only <c>Authorized users</c> can delete a favorite.</remarks>
        /// <response code="204">Favorite deleted successfully.</response>
        /// <response code="401">Unauthorized. User is not authenticated.</response>
        /// <response code="404">Favorite not found.</response>
        [Authorize]
        [HttpDelete("delete/{studentUsername}/{teacherUsername}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteFavorite(string studentUsername, string teacherUsername)
        {
            var isDeleted = await _FavoriteService.DeleteAsync(studentUsername, teacherUsername);
            if (!isDeleted)
                return NotFound(ApiResponse<object>.Failure("Favorite not found."));
            return NoContent();
        }
    }
}
