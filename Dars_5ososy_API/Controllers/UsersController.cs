using Dars_5ososy_API.Application.DTOs.UserDTOs;
using Dars_5ososy_API.Application.Services;
using Dars_5ososy_API.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dars_5ososy_API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        /// <summary>Get the details of the currently authenticated user.</summary>
        /// <remarks>Only <c>Authorized users</c> can access this endpoint.</remarks>
        /// <response code="200">User details retrieved successfully.</response>
        /// <response code="401">User not authenticated.</response>
        /// <response code="404">User not found.</response>
        [Authorize]
        [HttpGet("me")]
        [ProducesResponseType(typeof(ApiResponse<UserDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMe()
        {
            var username = User.Identity?.Name;

            if (username == null)
                return Unauthorized(ApiResponse<object>.Fail("User not authenticated"));

            var user = await _userService.GetByUserNameAsync(username);

            if (user == null)
                return NotFound(ApiResponse<object>.Fail("User not found"));

            return Ok(ApiResponse<UserDTO>.Succeeded(user, "User details retrieved successfully"));
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
                return NotFound(ApiResponse<object>.Fail("User not found"));
            return Ok(ApiResponse<UserDTO>.Succeeded(user, "User details retrieved successfully"));
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
                return NotFound(ApiResponse<object>.Fail("User not found"));
            return Ok(ApiResponse<UserDTO>.Succeeded(user, "User details retrieved successfully"));
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
                return NotFound(ApiResponse<object>.Fail("User not found"));
            return Ok(ApiResponse<UserDTO>.Succeeded(user, "User details retrieved successfully"));
        }

        /// <summary>Get all users.</summary>
        /// <remarks>Only <c>Authorized users</c> can access this endpoint.</remarks>
        /// <response code="200">Users retrieved successfully.</response>
        /// <response code="404">No users found.</response>
        [HttpGet("get-all")]
        [ProducesResponseType(typeof(ApiResponse<List<UserDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllAsync();
            if (users == null || !users.Any())
                return NotFound(ApiResponse<object>.Fail("No users found"));
            return Ok(ApiResponse<List<UserDTO>>.Succeeded(users, "All users retrieved successfully"));
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
                return BadRequest(ApiResponse<object>.Fail("User creation failed"));
            return Ok(ApiResponse<UserDTO>.Succeeded(createdUser, "User created successfully"));
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
                return BadRequest(ApiResponse<object>.Fail("User update failed"));
            return Ok(ApiResponse<UserDTO>.Succeeded(updatedUser, "User updated successfully"));
        }

        /// <summary>Delete an existing user.</summary>
        /// <remarks>Only <c>Authorized users</c> can delete a user.</remarks>
        /// <response code="200">User deleted successfully.</response>
        /// <response code="400">User deletion failed.</response>
        [Authorize]
        [HttpDelete("delete/{id}")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var result = await _userService.DeleteAsync(id);
            if (!result)
                return BadRequest(ApiResponse<object>.Fail("User deletion failed"));
            return Ok(ApiResponse<object>.Succeeded(null, "User deleted successfully"));
        }
    }
}
