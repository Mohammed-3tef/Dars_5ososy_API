using Dars_5ososy_API.Application.DTOs;
using Dars_5ososy_API.Application.Services;
using Dars_5ososy_API.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dars_5ososy_API.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly ReviewService _ReviewService;

        public ReviewsController(ReviewService ReviewService)
        {
            _ReviewService = ReviewService;
        }

        /// <summary>Get all reviews.</summary>
        /// <response code="200">Reviews retrieved successfully.</response>
        /// <response code="404">No reviews found.</response>
        [HttpGet("get-all")]
        [ProducesResponseType(typeof(ApiResponse<List<ReviewDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllReviews()
        {
            var Reviews = await _ReviewService.GetAllAsync();
            if (Reviews == null || !Reviews.Any())
                return NotFound(ApiResponse<object>.Fail("No reviews found."));
            return Ok(ApiResponse<List<ReviewDTO>>.Succeeded(Reviews, "Reviews retrieved successfully."));
        }

        /// <summary>Get reviews by student username.</summary>
        /// <response code="200">Reviews retrieved successfully.</response>
        /// <response code="404">No reviews found for the specified student.</response>
        [HttpGet("get-by-student/{studentUsername}")]
        [ProducesResponseType(typeof(ApiResponse<List<ReviewDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetReviewsByStudentUsername(string studentUsername)
        {
            var Reviews = await _ReviewService.GetByStudentUsernameAsync(studentUsername);
            if (Reviews == null || !Reviews.Any())
                return NotFound(ApiResponse<object>.Fail("No reviews found for the specified student."));

            return Ok(ApiResponse<List<ReviewDTO>>.Succeeded(Reviews, "Reviews retrieved successfully."));
        }

        /// <summary>Get reviews by teacher username.</summary>
        /// <response code="200">Reviews retrieved successfully.</response>
        /// <response code="404">No reviews found for the specified teacher.</response>
        [HttpGet("get-by-teacher/{teacherUsername}")]
        [ProducesResponseType(typeof(ApiResponse<List<ReviewDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetReviewsByTeacherUsername(string teacherUsername)
        {
            var Reviews = await _ReviewService.GetByTeacherUsernameAsync(teacherUsername);
            if (Reviews == null || !Reviews.Any())
                return NotFound(ApiResponse<object>.Fail("No reviews found for the specified teacher."));
            return Ok(ApiResponse<List<ReviewDTO>>.Succeeded(Reviews, "Reviews retrieved successfully."));
        }

        /// <summary>Get a review by student and teacher usernames.</summary>
        /// <response code="200">Review retrieved successfully.</response>
        /// <response code="404">No review found for the specified student and teacher.</response>
        [HttpGet("get/{studentUsername}/{teacherUsername}")]
        [ProducesResponseType(typeof(ApiResponse<ReviewDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetReviewByStudentAndTeacher(string studentUsername, string teacherUsername)
        {
            var review = await _ReviewService.GetByStudentAndTeacherAsync(studentUsername, teacherUsername);
            if (review == null)
                return NotFound(ApiResponse<object>.Fail("No review found for the specified student and teacher."));

            return Ok(ApiResponse<ReviewDTO>.Succeeded(review, "Review retrieved successfully."));
        }

        /// <summary>Create a new review.</summary>
        /// <remarks>Only <c>Authorized users</c> can create a new review.</remarks>
        /// <response code="201">Review created successfully.</response>
        /// <response code="400">Review with the same details already exists.</response>
        [Authorize]
        [HttpPost("create")]
        [ProducesResponseType(typeof(ApiResponse<ReviewDTO>), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateReview(ReviewDTO ReviewDto)
        {
            var createdReview = await _ReviewService.CreateAsync(ReviewDto);
            if (createdReview == null)
                return BadRequest(ApiResponse<object>.Fail("Review with the same details already exists."));

            return CreatedAtAction(nameof(GetAllReviews), ApiResponse<ReviewDTO>.Succeeded(createdReview, "Review created successfully."));
        }

        /// <summary>Update an existing review.</summary>
        /// <remarks>Only <c>Authorized users</c> can update a review.</remarks>
        /// <response code="200">Review updated successfully.</response>
        /// <response code="404">Review not found.</response>
        [Authorize]
        [HttpPut("update")]
        [ProducesResponseType(typeof(ApiResponse<ReviewDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateReview(ReviewDTO ReviewDto)
        {
            var updatedReview = await _ReviewService.UpdateAsync(ReviewDto);
            if (updatedReview == null)
                return NotFound(ApiResponse<object>.Fail("Review not found."));

            return Ok(ApiResponse<ReviewDTO>.Succeeded(updatedReview, "Review updated successfully."));
        }

        /// <summary>Delete a review by student and teacher usernames.</summary>
        /// <remarks>Only <c>Authorized users</c> can delete a review.</remarks>
        /// <response code="200">Review deleted successfully.</response>
        /// <response code="404">Review not found.</response>
        [Authorize]
        [HttpDelete("delete/{studentUsername}/{teacherUsername}")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteReview(string studentUsername, string teacherUsername)
        {
            var isDeleted = await _ReviewService.DeleteAsync(studentUsername, teacherUsername);
            if (!isDeleted)
                return NotFound(ApiResponse<object>.Fail("Review not found."));

            return Ok(ApiResponse<object>.Succeeded(null, "Review deleted successfully."));
        }
    }
}
