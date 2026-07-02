using Asp.Versioning;
using Dars_5ososy_API.Application.DTOs;
using Dars_5ososy_API.Application.Services;
using Dars_5ososy_API.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dars_5ososy_API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/availability-slots")]
    [ApiController]
    public class AvailabilitySlotsController : ControllerBase
    {
        private readonly AvailabilitySlotService _availabilitySlotService;

        public AvailabilitySlotsController(AvailabilitySlotService availabilitySlotService)
        {
            _availabilitySlotService = availabilitySlotService;
        }

        /// <summary>Get all availability slots.</summary>
        /// <response code="200">Availability slots retrieved successfully.</response>
        /// <response code="404">No availability slots found.</response>
        [HttpGet("get-all")]
        [ProducesResponseType(typeof(ApiResponse<List<AvailabilitySlotDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var availabilitySlots = await _availabilitySlotService.GetAllAvailabilitySlotsAsync();
            if (availabilitySlots == null || !availabilitySlots.Any())
                return NotFound(ApiResponse<List<AvailabilitySlotDTO>>.Fail("No availability slots found."));
            return Ok(ApiResponse<List<AvailabilitySlotDTO>>.Successed(availabilitySlots, "Availability slots retrieved successfully."));
        }

        /// <summary>Get availability slots by teacher username.</summary>
        /// <response code="200">Availability slots retrieved successfully.</response>
        /// <response code="404">No availability slots found for the specified teacher.</response>
        [HttpGet("get-by-teacher-username/{teacherUsername}")]
        [ProducesResponseType(typeof(ApiResponse<List<AvailabilitySlotDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByTeacherUsername(string teacherUsername)
        {
            var availabilitySlots = await _availabilitySlotService.GetAvailabilitySlotsByTeacherUsernameAsync(teacherUsername);
            if (availabilitySlots == null || !availabilitySlots.Any())
                return NotFound(ApiResponse<List<AvailabilitySlotDTO>>.Fail($"No availability slots found for teacher with username '{teacherUsername}'."));
            return Ok(ApiResponse<List<AvailabilitySlotDTO>>.Successed(availabilitySlots, $"Availability slots for teacher '{teacherUsername}' retrieved successfully."));
        }

        /// <summary>Get a specific availability slot by teacher username, day of the week, and start time.</summary>
        /// <response code="200">Availability slot retrieved successfully.</response>
        /// <response code="404">No availability slot found for the specified criteria.</response>
        [HttpGet("get-specific-by-teacher-username/{teacherUsername}/{dayOfWeek}/{startTime}")]
        [ProducesResponseType(typeof(ApiResponse<AvailabilitySlotDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSpecificByTeacherUsername(string teacherUsername, string dayOfWeek, TimeOnly startTime)
        {
            var availabilitySlot = await _availabilitySlotService.GetSpecificAvailabilitySlotByTeacherUsernameAsync(teacherUsername, dayOfWeek, startTime);
            if (availabilitySlot == null)
                return NotFound(ApiResponse<AvailabilitySlotDTO>.Fail($"No availability slot found for teacher '{teacherUsername}' on '{dayOfWeek}' at '{startTime}'."));
            return Ok(ApiResponse<AvailabilitySlotDTO>.Successed(availabilitySlot, $"Availability slot for teacher '{teacherUsername}' on '{dayOfWeek}' at '{startTime}' retrieved successfully."));
        }

        /// <summary>Create a new availability slot.</summary>
        /// <remarks>Only users with the <c>Admin</c> or <c>Teacher</c> role can create an availability slot.</remarks>
        /// <response code="201">Availability slot created successfully.</response>
        /// <response code="400">Failed to create availability slot.</response>
        /// <response code="401">Unauthorized. User is not authenticated.</response>
        /// <response code="403">Forbidden. User does not have the required role.</response>
        /// <response code="409">An availability slot already exists for the specified teacher, day of the week, and start time.</response>
        [Authorize(Roles = "Admin,Teacher")]
        [HttpPost("create")]
        [ProducesResponseType(typeof(ApiResponse<AvailabilitySlotDTO>), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(AvailabilitySlotDTO availabilitySlotDTO)
        {
            var existingSlot = await _availabilitySlotService
                .GetSpecificAvailabilitySlotByTeacherUsernameAsync(availabilitySlotDTO.TeacherUsername, availabilitySlotDTO.DayOfWeek, availabilitySlotDTO.StartTime);
            if (existingSlot != null)
                return Conflict(ApiResponse<AvailabilitySlotDTO>.Fail($"An availability slot already exists for teacher '{availabilitySlotDTO.TeacherUsername}' on '{availabilitySlotDTO.DayOfWeek}' at '{availabilitySlotDTO.StartTime}'."));
            var createdAvailabilitySlot = await _availabilitySlotService.CreateAvailabilitySlotAsync(availabilitySlotDTO);
            if (createdAvailabilitySlot == null)
                return BadRequest(ApiResponse<AvailabilitySlotDTO>.Fail("Failed to create availability slot."));
            return CreatedAtAction(nameof(GetSpecificByTeacherUsername), new { teacherUsername = createdAvailabilitySlot.TeacherUsername, dayOfWeek = createdAvailabilitySlot.DayOfWeek, startTime = createdAvailabilitySlot.StartTime }, ApiResponse<AvailabilitySlotDTO>.Successed(createdAvailabilitySlot, "Availability slot created successfully."));
        }

        /// <summary>Update an existing availability slot.</summary>
        /// <remarks>Only users with the <c>Admin</c> or <c>Teacher</c> role can update an availability slot.</remarks>
        /// <response code="200">Availability slot updated successfully.</response>
        /// <response code="400">Failed to update availability slot.</response>
        /// <response code="401">Unauthorized. User is not authenticated.</response>
        /// <response code="403">Forbidden. User does not have the required role.</response>
        /// <response code="404">Availability slot not found.</response>
        [Authorize(Roles = "Admin,Teacher")]
        [HttpPut("update")]
        [ProducesResponseType(typeof(ApiResponse<AvailabilitySlotDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(AvailabilitySlotDTO availabilitySlotDTO)
        {
            var existingSlot = await _availabilitySlotService
                .GetSpecificAvailabilitySlotByTeacherUsernameAsync(availabilitySlotDTO.TeacherUsername, availabilitySlotDTO.DayOfWeek, availabilitySlotDTO.StartTime);
            if (existingSlot == null)
                return NotFound(ApiResponse<AvailabilitySlotDTO>.Fail("Availability slot not found."));
            var updatedAvailabilitySlot = await _availabilitySlotService.UpdateAvailabilitySlotAsync(availabilitySlotDTO);
            if (updatedAvailabilitySlot == null)
                return BadRequest(ApiResponse<AvailabilitySlotDTO>.Fail("Failed to update availability slot."));
            return Ok(ApiResponse<AvailabilitySlotDTO>.Successed(updatedAvailabilitySlot, "Availability slot updated successfully."));
        }

        /// <summary>Delete an existing availability slot.</summary>
        /// <remarks>Only users with the <c>Admin</c> or <c>Teacher</c> role can delete an availability slot.</remarks>
        /// <response code="204">Availability slot deleted successfully.</response>
        /// <response code="400">Failed to delete availability slot.</response>
        /// <response code="401">Unauthorized. User is not authenticated.</response>
        /// <response code="403">Forbidden. User does not have the required role.</response>
        /// <response code="404">Availability slot not found.</response>
        [Authorize(Roles = "Admin,Teacher")]
        [HttpDelete("delete/{teacherUsername}/{dayOfWeek}/{startTime}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(string teacherUsername, string dayOfWeek, TimeOnly startTime)
        {
            var existingSlot = await _availabilitySlotService
                .GetSpecificAvailabilitySlotByTeacherUsernameAsync(teacherUsername, dayOfWeek, startTime);
            if (existingSlot == null)
                return NotFound(ApiResponse<AvailabilitySlotDTO>.Fail($"No availability slot found for teacher '{teacherUsername}' on '{dayOfWeek}' at '{startTime}' to delete."));
            var isDeleted = await _availabilitySlotService.DeleteAvailabilitySlotAsync(teacherUsername, dayOfWeek, startTime);
            if (!isDeleted)
                return BadRequest(ApiResponse<bool>.Fail($"Failed to delete availability slot for teacher '{teacherUsername}' on '{dayOfWeek}' at '{startTime}'."));
            return NoContent();
        }
    }
}
