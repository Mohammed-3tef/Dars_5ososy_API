using Dars_5ososy_API.Application.DTOs.UserDTOs;
using Dars_5ososy_API.Application.Services;
using Dars_5ososy_API.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dars_5ososy_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetMe()
        {
            var username = User.Identity?.Name;

            if (username == null)
                return Unauthorized(ApiResponse<object>.Fail("User not authenticated"));

            var user = await _userService.GetByUserNameAsync(username);

            if (user == null)
                return NotFound(ApiResponse<object>.Fail("User not found"));

            return Ok(ApiResponse<object>.Succeeded(user, "User details retrieved successfully"));
        }

        [HttpGet("get-by-username/{userName}")]
        public async Task<IActionResult> GetUserByUserName(string userName)
        {
            var user = await _userService.GetByUserNameAsync(userName);

            if (user == null)
                return BadRequest(ApiResponse<object>.Fail("User not found"));

            return Ok(ApiResponse<object>.Succeeded(user, "User details retrieved successfully"));
        }

        [HttpGet("get-by-email/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _userService.GetByEmailAsync(email);

            if (user == null)
                return BadRequest(ApiResponse<object>.Fail("User not found"));

            return Ok(ApiResponse<object>.Succeeded(user, "User details retrieved successfully"));
        }

        [HttpGet("get-by-phone/{phoneNumber}")]
        public async Task<IActionResult> GetUserByPhoneNumber(string phoneNumber)
        {
            var user = await _userService.GetByPhoneNumberAsync(phoneNumber);

            if (user == null)
                return BadRequest(ApiResponse<object>.Fail("User not found"));

            return Ok(ApiResponse<object>.Succeeded(user, "User details retrieved successfully"));
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllAsync();
            if (users == null || !users.Any())
                return NotFound(ApiResponse<object>.Fail("No users found"));
            return Ok(ApiResponse<object>.Succeeded(users, "All users retrieved successfully"));
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser(CreatedUserDTO createdUserDTO)
        {
            var createdUser = await _userService.CreateAsync(createdUserDTO, createdUserDTO.Password);
            if (createdUser == null)
                return BadRequest(ApiResponse<object>.Fail("User creation failed"));
            return Ok(ApiResponse<object>.Succeeded(createdUser, "User created successfully"));
        }

        [HttpPut("update")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(UpdatedUserDTO userDto)
        {
            var updatedUser = await _userService.UpdateAsync(userDto);
            if (updatedUser == null)
                return BadRequest(ApiResponse<object>.Fail("User update failed"));
            return Ok(ApiResponse<object>.Succeeded(updatedUser, "User updated successfully"));
        }

        [HttpDelete("delete/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var result = await _userService.DeleteAsync(id);
            if (!result)
                return BadRequest(ApiResponse<object>.Fail("User deletion failed"));
            return Ok(ApiResponse<object>.Succeeded(null, "User deleted successfully"));
        }
    }
}
