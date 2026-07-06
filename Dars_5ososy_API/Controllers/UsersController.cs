using Asp.Versioning;
using Dars_5ososy_API.Application.DTOs.UserDTOs;
using Dars_5ososy_API.Application.Services;
using Dars_5ososy_API.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dars_5ososy_API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        /// <summary>Get a user by username.</summary>
        /// <remarks>Only <c>Authorized users</c> can access this endpoint.</remarks>
        /// <response code="200">User details retrieved successfully.</response>
        /// <response code="404">User not found.</response>
        [HttpGet("get-by-username/{userName}")]
        [ProducesResponseType(typeof(ApiResponse<UserDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserByUserName(string userName)
        {
            var user = await _userService.GetByUserNameAsync(userName);
            if (user == null)
                return NotFound(ApiResponse<object>.Failure("User not found"));
            return Ok(ApiResponse<UserDTO>.Success(user, "User details retrieved successfully"));
        }

        /// <summary>Get a user by email.</summary>
        /// <remarks>Only <c>Authorized users</c> can access this endpoint.</remarks>
        /// <response code="200">User details retrieved successfully.</response>
        /// <response code="404">User not found.</response>
        [HttpGet("get-by-email/{email}")]
        [ProducesResponseType(typeof(ApiResponse<UserDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _userService.GetByEmailAsync(email);
            if (user == null)
                return NotFound(ApiResponse<object>.Failure("User not found"));
            return Ok(ApiResponse<UserDTO>.Success(user, "User details retrieved successfully"));
        }

        /// <summary>Get a user by phone number.</summary>
        /// <remarks>Only <c>Authorized users</c> can access this endpoint.</remarks>
        /// <response code="200">User details retrieved successfully.</response>
        /// <response code="404">User not found.</response>
        [HttpGet("get-by-phone/{phoneNumber}")]
        [ProducesResponseType(typeof(ApiResponse<UserDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserByPhoneNumber(string phoneNumber)
        {
            var user = await _userService.GetByPhoneNumberAsync(phoneNumber);
            if (user == null)
                return NotFound(ApiResponse<object>.Failure("User not found"));
            return Ok(ApiResponse<UserDTO>.Success(user, "User details retrieved successfully"));
        }

        /// <summary>Get all students.</summary>
        /// <remarks>Only <c>Authorized users</c> can access this endpoint.</remarks>
        /// <response code="200">Students retrieved successfully.</response>
        /// <response code="404">No students found.</response>
        [HttpGet("get-all-students")]
        [ProducesResponseType(typeof(ApiResponse<List<UserDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllStudents()
        {
            var users = await _userService.GetAllStudentsAsync();
            if (users == null || !users.Any())
                return NotFound(ApiResponse<object>.Failure("No students found"));
            return Ok(ApiResponse<List<UserDTO>>.Success(users, "All students retrieved successfully"));
        }

        /// <summary>Get all teachers.</summary>
        /// <remarks>Only <c>Authorized users</c> can access this endpoint.</remarks>
        /// <response code="200">Teachers retrieved successfully.</response>
        /// <response code="404">No teachers found.</response>
        [HttpGet("get-all-teachers")]
        [ProducesResponseType(typeof(ApiResponse<List<UserDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllTeachers()
        {
            var users = await _userService.GetAllTeachersAsync();
            if (users == null || !users.Any())
                return NotFound(ApiResponse<object>.Failure("No teachers found"));
            return Ok(ApiResponse<List<UserDTO>>.Success(users, "All teachers retrieved successfully"));
        }

        /// <summary>Create a new user.</summary>
        /// <remarks>Only users with the <c>Admin</c> role can create a user.</remarks>
        /// <response code="201">User created successfully.</response>
        /// <response code="400">User creation failed.</response>
        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ApiResponse<UserDTO>), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateUser(CreatedUserDTO createdUserDTO)
        {
            var createdUser = await _userService.CreateAsync(createdUserDTO, createdUserDTO.Password);
            if (createdUser == null)
                return BadRequest(ApiResponse<object>.Failure("User creation failed"));
            return Ok(ApiResponse<UserDTO>.Success(createdUser, "User created successfully"));
        }

        /// <summary>Update an existing user.</summary>
        /// <remarks>Only <c>Authorized users</c> can update a user.</remarks>
        /// <response code="200">User updated successfully.</response>
        /// <response code="400">User update failed.</response>
        [Authorize]
        [HttpPut("update")]
        [ProducesResponseType(typeof(ApiResponse<UserDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateUser(UpdatedUserDTO userDto)
        {
            var updatedUser = await _userService.UpdateAsync(userDto);
            if (updatedUser == null)
                return BadRequest(ApiResponse<object>.Failure("User update failed"));
            return Ok(ApiResponse<UserDTO>.Success(updatedUser, "User updated successfully"));
        }

        /// <summary>Delete an existing user.</summary>
        /// <remarks>Only <c>Authorized users</c> can delete a user.</remarks>
        /// <response code="204">User deleted successfully.</response>
        /// <response code="400">User deletion failed.</response>
        [Authorize]
        [HttpDelete("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var result = await _userService.DeleteAsync(id);
            if (!result)
                return BadRequest(ApiResponse<object>.Failure("User deletion failed"));
            return NoContent();
        }
    }
}
