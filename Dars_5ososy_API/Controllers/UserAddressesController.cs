using Asp.Versioning;
using Dars_5ososy_API.Application.DTOs.UserDTOs;
using Dars_5ososy_API.Application.Services;
using Dars_5ososy_API.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dars_5ososy_API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/user-addresses")]
    [ApiController]
    public class UserAddressesController : ControllerBase
    {
        private readonly UserAddressService _userAddressService;
        
        public UserAddressesController(UserAddressService userAddressService)
        {
            _userAddressService = userAddressService;
        }

        /// <summary>Get all user addresses</summary>
        /// <response code="200">User addresses retrieved successfully.</response>
        /// <response code="404">No user addresses found.</response>
        [HttpGet("get-all")]
        [ProducesResponseType(typeof(ApiResponse<List<UserAddressDTO>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<UserAddressDTO>>> GetAllUserAddresses()
        {
            var userAddresses = await _userAddressService.GetAllUserAddressesAsync();
            if (userAddresses == null || !userAddresses.Any())
                return NotFound(ApiResponse<object>.Failure("No user addresses found."));
            return Ok(ApiResponse<List<UserAddressDTO>>.Success(userAddresses, "User addresses retrieved successfully."));
        }

        /// <summary>Get user address by username</summary>
        /// <response code="200">User address retrieved successfully.</response>
        /// <response code="404">User address not found.</response>
        [HttpGet("get-by-username/{username}")]
        [ProducesResponseType(typeof(ApiResponse<UserAddressDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<UserAddressDTO>> GetUserAddressByUsername(string username)
        {
            var userAddress = await _userAddressService.GetUserAddressByUsernameAsync(username);
            if (userAddress == null)
                return NotFound(ApiResponse<object>.Failure("User address not found."));
            return Ok(ApiResponse<UserAddressDTO>.Success(userAddress, "User address retrieved successfully."));
        }

        /// <summary>Create a new user address</summary>
        /// <response code="201">User address created successfully.</response>
        /// <response code="400">User address already exists. / User address creation failed.</response>
        /// <response code="401">Unauthorized. User is not authenticated.</response>
        [HttpPost("create")]
        [ProducesResponseType(typeof(ApiResponse<UserAddressDTO>), StatusCodes.Status201Created)]
        public async Task<ActionResult<UserAddressDTO>> CreateUserAddress(UserAddressDTO userAddressDto)
        {
            var existingUserAddress = await _userAddressService.GetUserAddressByUsernameAsync(userAddressDto.UserName);
            if (existingUserAddress == null)
                return BadRequest(ApiResponse<object>.Failure("User address already exists."));

            var createdUserAddress = await _userAddressService.CreateUserAddressAsync(userAddressDto);
            if (createdUserAddress == null)
                return BadRequest(ApiResponse<object>.Failure("User address creation failed."));

            return CreatedAtAction(nameof(GetUserAddressByUsername), new { username = createdUserAddress.UserName }, ApiResponse<UserAddressDTO>.Success(createdUserAddress, "User address created successfully."));
        }

        /// <summary>Update an existing user address</summary>
        /// <remarks>Only <c>Authorized users</c> can access this endpoint.</remarks>
        /// <response code="200">User address updated successfully.</response>
        /// <response code="400">User address update failed.</response>
        /// <response code="401">Unauthorized. User is not authenticated.</response>
        /// <response code="404">User address not found.</response>
        [Authorize]
        [HttpPut("update")]
        [ProducesResponseType(typeof(ApiResponse<UserAddressDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<UserAddressDTO>> UpdateUserAddress(UserAddressDTO userAddressDto)
        {
            var existingUserAddress = await _userAddressService.GetUserAddressByUsernameAsync(userAddressDto.UserName);
            if (existingUserAddress == null)
                return NotFound(ApiResponse<object>.Failure("User address not found."));

            var updatedUserAddress = await _userAddressService.UpdateUserAddressAsync(userAddressDto);
            if (updatedUserAddress == null)
                return BadRequest(ApiResponse<object>.Failure("User address update failed."));

            return Ok(ApiResponse<UserAddressDTO>.Success(updatedUserAddress, "User address updated successfully."));
        }

        /// <summary>Delete a user address by username</summary>
        /// <remarks>Only <c>Authorized users</c> can access this endpoint.</remarks>
        /// <response code="204">User address deleted successfully.</response>
        /// <response code="400">User address deletion failed.</response>
        /// <response code="401">Unauthorized. User is not authenticated.</response>
        /// <response code="404">User address not found.</response>
        [Authorize]
        [HttpDelete("delete/{username}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteUserAddress(string username)
        {
            var existingUserAddress = await _userAddressService.GetUserAddressByUsernameAsync(username);
            if (existingUserAddress == null)
                return NotFound(ApiResponse<object>.Failure("User address not found."));

            var deleted = await _userAddressService.DeleteUserAddressAsync(username);
            if (!deleted)
                return BadRequest(ApiResponse<object>.Failure("User address deletion failed."));

            return NoContent();
        }
    }
}
