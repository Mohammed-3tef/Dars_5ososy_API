using Asp.Versioning;
using Dars_5ososy_API.Application.DTOs;
using Dars_5ososy_API.Application.Services;
using Dars_5ososy_API.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dars_5ososy_API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/bookings")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly BookingService _bookingService;

        public BookingsController(BookingService bookingService)
        {
            _bookingService = bookingService;
        }

        /// <summary>Get all bookings.</summary>
        /// <response code="200">Bookings retrieved successfully.</response>
        /// <response code="404">No bookings found.</response>
        [HttpGet("get-all")]
        [ProducesResponseType(typeof(ApiResponse<List<BookingDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllbookings()
        {
            var bookings = await _bookingService.GetAllAsync();
            if (bookings == null || !bookings.Any())
                return NotFound(ApiResponse<object>.Failure("No bookings found."));
            return Ok(ApiResponse<List<BookingDTO>>.Success(bookings, "Bookings retrieved successfully."));
        }

        /// <summary>Get bookings by student username.</summary>
        /// <response code="200">Bookings retrieved successfully.</response>
        /// <response code="404">No bookings found for the specified student.</response>
        [HttpGet("get-by-student/{studentUsername}")]
        [ProducesResponseType(typeof(ApiResponse<List<BookingDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetbookingsByStudentUsername(string studentUsername)
        {
            var bookings = await _bookingService.GetByStudentUsernameAsync(studentUsername);
            if (bookings == null || !bookings.Any())
                return NotFound(ApiResponse<object>.Failure("No bookings found for the specified student."));

            return Ok(ApiResponse<List<BookingDTO>>.Success(bookings, "Bookings retrieved successfully."));
        }

        /// <summary>Get bookings by teacher username.</summary>
        /// <response code="200">Bookings retrieved successfully.</response>
        /// <response code="404">No bookings found for the specified teacher.</response>
        [HttpGet("get-by-teacher/{teacherUsername}")]
        [ProducesResponseType(typeof(ApiResponse<List<BookingDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetbookingsByTeacherUsername(string teacherUsername)
        {
            var bookings = await _bookingService.GetByTeacherUsernameAsync(teacherUsername);
            if (bookings == null || !bookings.Any())
                return NotFound(ApiResponse<object>.Failure("No bookings found for the specified teacher."));
            return Ok(ApiResponse<List<BookingDTO>>.Success(bookings, "Bookings retrieved successfully."));
        }

        /// <summary>Get a booking by student and teacher usernames.</summary>
        /// <response code="200">Booking retrieved successfully.</response>
        /// <response code="404">No booking found for the specified student and teacher.</response>
        [HttpGet("get/{studentUsername}/{teacherUsername}")]
        [ProducesResponseType(typeof(ApiResponse<BookingDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetbookingByStudentAndTeacher(string studentUsername, string teacherUsername)
        {
            var booking = await _bookingService.GetByStudentAndTeacherAsync(studentUsername, teacherUsername);
            if (booking == null)
                return NotFound(ApiResponse<object>.Failure("No booking found for the specified student and teacher."));
            return Ok(ApiResponse<BookingDTO>.Success(booking, "Booking retrieved successfully."));
        }

        /// <summary>Create a new booking.</summary>
        /// <remarks>Only <c>Authorized users</c> can create a new booking.</remarks>
        /// <response code="201">Booking created successfully.</response>
        /// <response code="400">Failed to create booking.</response>
        /// <response code="401">Unauthorized. User is not authenticated.</response>
        /// <response code="409">Booking with the same details already exists.</response>
        [Authorize]
        [HttpPost("create")]
        [ProducesResponseType(typeof(ApiResponse<BookingDTO>), StatusCodes.Status201Created)]
        public async Task<IActionResult> Createbooking(BookingDTO BookingDTO)
        {
            var existingbooking = await _bookingService.GetByStudentAndTeacherAsync(BookingDTO.StudentUsername, BookingDTO.AvailabilitySlot.TeacherUsername);
            if (existingbooking != null)
                return Conflict(ApiResponse<object>.Failure("Booking with the same details already exists."));
            var createdbooking = await _bookingService.CreateAsync(BookingDTO);
            if (createdbooking == null)
                return BadRequest(ApiResponse<object>.Failure("Failed to create booking."));
            return CreatedAtAction(nameof(GetAllbookings), ApiResponse<BookingDTO>.Success(createdbooking, "Booking created successfully."));
        }

        /// <summary>Delete a booking by student and teacher usernames.</summary>
        /// <remarks>Only <c>Authorized users</c> can delete a booking.</remarks>
        /// <response code="204">Booking deleted successfully.</response>
        /// <response code="400">Failed to delete booking.</response>
        /// <response code="401">Unauthorized. User is not authenticated.</response>
        /// <response code="404">Booking not found.</response>
        [Authorize]
        [HttpDelete("delete/{studentUsername}/{teacherUsername}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Deletebooking(string studentUsername, string teacherUsername)
        {
            var existingbooking = await _bookingService.GetByStudentAndTeacherAsync(studentUsername, teacherUsername);
            if (existingbooking == null)
                return NotFound(ApiResponse<object>.Failure("Booking not found."));
            var isDeleted = await _bookingService.DeleteAsync(studentUsername, teacherUsername);
            if (!isDeleted)
                return BadRequest(ApiResponse<object>.Failure("Failed to delete booking."));
            return NoContent();
        }
    }
}
