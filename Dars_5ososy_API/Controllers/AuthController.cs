using Asp.Versioning;
using AutoMapper;
using Dars_5ososy_API.Application.DTOs;
using Dars_5ososy_API.Application.DTOs.AuthDTOs;
using Dars_5ososy_API.Application.DTOs.UserDTOs;
using Dars_5ososy_API.Application.Services;
using Dars_5ososy_API.Domain.Entities;
using Dars_5ososy_API.Infrastructure;
using Dars_5ososy_API.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Web;

namespace Dars_5ososy_API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private const string ApiVersion = "1.0";
        private readonly IMapper _mapper;
        private readonly AuthService _authService;
        private readonly UserManager<User> _userManager;
        private readonly UserService _userService;
        private readonly TokenService _tokenService;
        private readonly EmailService _emailService;
        private readonly AppDbContext _db;

        public AuthController(
            IMapper mapper, 
            AuthService authService,
            UserManager<User> userManager,
            UserService userService,
            TokenService tokenService, 
            EmailService emailService, 
            AppDbContext db)
        {
            _mapper = mapper;
            _authService = authService;
            _userManager = userManager;
            _userService = userService;
            _tokenService = tokenService;
            _emailService = emailService;
            _db = db;
        }

        /// <summary>Register a new user.</summary>
        /// <response code="200">User registered successfully.</response>
        /// <response code="400">User registration failed.</response>
        [HttpPost("register")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Register(CreatedUserDTO dto)
        {
            var result = await _authService.RegisterUserAsync(dto);

            if (!result)
                return BadRequest(ApiResponse<object>.Failure($"{(dto.IsStudent ? "Student" : "Teacher")} creation failed"));

            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return BadRequest(ApiResponse<object>.Failure("User registration failed"));

            var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var validToken = HttpUtility.UrlEncode(confirmationToken);

            var confirmationLink = Url.Action(
                "ConfirmEmail",
                "Auth",
                new { version = ApiVersion, email = user.Email, token = validToken },
                Request.Scheme
            );

            string confirmEmailBody = "Please confirm your email address to activate your Dars 5ososy account and unlock all features.\n\nThis step is required to verify that this email belongs to you and to keep your account secure.\n\nClick the button below to confirm your email address.";

            EmailRequestDTO emailRequest = new EmailRequestDTO
            {
                ToEmail = user.Email,
                Subject = "Confirm your email",
                Body = EmailService.CreateEmailBody("Confirm your email", confirmEmailBody, confirmationLink),
                IsHtml = true
            };
            await _emailService.SendEmailAsync(emailRequest);

            var token = await _tokenService.CreateToken(user);

            UserDTO userDTO = _mapper.Map<UserDTO>(user);
            return Ok(ApiResponse<object>.Success(new
                {
                    token, 
                    user = userDTO
                }, "User registered successfully. Please check your email to confirm."));
        }

        /// <summary>Login a user.</summary>
        /// <response code="200">Login successful.</response>
        /// <response code="401">Invalid email or password.</response>
        [HttpPost("login")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        public async Task<IActionResult> LoginAsync(LoginDTO dto)
        {
            var user = await _userService.GetByEmailAsync(dto.Email);

            if (user == null)
                return Unauthorized(ApiResponse<object>.Failure("Invalid email or password"));

            var checkPassword = await _authService.CheckPasswordAsync(dto);

            if (!checkPassword)
                return Unauthorized(ApiResponse<object>.Failure("Invalid email or password"));

            var tokens = await _tokenService.CreateTokenPair(dto.Email);

            return Ok(ApiResponse<object>.Success(new
                {
                    token = tokens.AccessToken,
                    refreshToken = tokens.RefreshToken,
                    user
                }, "Login successful"));
        }

        /// <summary>Refresh a user's token.</summary>
        /// <response code="200">Refresh token refreshed successfully.</response>
        /// <response code="400">Refresh token is required.</response>
        /// <response code="401">Invalid refresh token.</response>
        [HttpPost("refresh")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Refresh(RefreshRequestDTO dto)
        {
            if (string.IsNullOrEmpty(dto?.RefreshToken))
                return BadRequest(ApiResponse<object>.Failure("Refresh token is required"));

            var existing = await _db.RefreshTokens
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Token == dto.RefreshToken && x.Revoked == null && x.Expires > DateTime.UtcNow);

            if (existing == null)
                return Unauthorized(ApiResponse<object>.Failure("Invalid refresh token"));

            // Revoke old token
            existing.Revoked = DateTime.UtcNow;
            await _db.SaveChangesAsync();

            var user = existing.User;
            var tokens = await _tokenService.CreateTokenPair(user.Email);

            return Ok(ApiResponse<object>.Success(new
                {
                    token = tokens.AccessToken,
                    refreshToken = tokens.RefreshToken
                }, "Token refreshed successfully")
            );
        }

        /// <summary>Confirm a user's email.</summary>
        /// <response code="200">Email confirmed successfully.</response>
        /// <response code="400">Invalid email confirmation.</response>
        /// <response code="404">User not found.</response>
        [HttpGet("confirm-email")]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ConfirmEmail(string email, string token)
        {
            var user = await _userService.GetByEmailAsync(email);

            if (user == null)
                return NotFound(ApiResponse<object>.Failure("User not found"));

            var result = await _authService.ConfirmEmailAsync(user, token);

            if (!result)
                return BadRequest(ApiResponse<object>.Failure("Invalid email confirmation"));

            return Ok(ApiResponse<string>.Success(new { Email = user.Email }, "Email confirmed successfully"));
        }

        /// <summary>Initiate a password reset request.</summary>
        /// <remarks>Only <c>Authorized users</c> can request a password reset.</remarks>
        /// <response code="200">Reset link sent successfully.</response>
        /// <response code="400">Invalid email address.</response>
        [Authorize]
        [HttpPost("forgot-password")]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var user = await _userService.GetByEmailAsync(email);

            if (user == null)
                return Ok(ApiResponse<object>.Success(string.Empty, "If the email exists, a reset link has been sent.")); // don't expose users

            var token = await _authService.GeneratePasswordResetTokenAsync(user);

            var resetLink = Url.Action(
                "ResetPassword",
                "Auth",
                new { version = ApiVersion, email, token },
                Request.Scheme
            );

            string resetEmailBody = "You're receiving this email because a request was made to reset your Dars 5ososy account password.\n\nFor your security, we need to verify this request before making any changes to your account.\n\nClick the button below to reset your password.";

            EmailRequestDTO emailRequest = new EmailRequestDTO
            {
                ToEmail = user.Email,
                Subject = "Reset your password",
                Body = EmailService.CreateEmailBody("Reset your password", resetEmailBody, resetLink),
                IsHtml = true
            };
            await _emailService.SendEmailAsync(emailRequest);

            return Ok(ApiResponse<string>.Success(string.Empty, "Reset link sent"));
        }

        /// <summary>Reset a user's password.</summary>
        /// <response code="200">Password reset successful.</response>
        /// <response code="400">Password reset failed.</response>
        /// <response code="404">User not found.</response>
        [HttpPost("reset-password")]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO dto)
        {
            var user = await _userService.GetByEmailAsync(dto.Email);

            if (user == null)
                return NotFound(ApiResponse<object>.Failure("User not found"));

            var decodedToken = WebUtility.UrlDecode(dto.Token);

            var result = await _authService
                .ResetPasswordAsync(user, decodedToken, dto.NewPassword);

            if (!result)
                return BadRequest(ApiResponse<object>.Failure("Password reset failed"));

            return Ok(ApiResponse<string>.Success(string.Empty, "Password reset successful"));
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
                return Unauthorized(ApiResponse<object>.Failure("User not authenticated"));

            var user = await _userService.GetByUserNameAsync(username);

            if (user == null)
                return NotFound(ApiResponse<object>.Failure("User not found"));

            return Ok(ApiResponse<UserDTO>.Success(user, "User details retrieved successfully"));
        }

        /// <summary>Logout the current user.</summary>
        /// <remarks>Only <c>Authorized users</c> can logout.</remarks>
        /// <response code="200">Logged out successfully.</response>
        /// <response code="401">User not found.</response>
        [Authorize]
        [HttpPost("logout")]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Logout()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return Unauthorized(ApiResponse<object>.Failure("User not found"));

            // Revoke all active refresh tokens for this user
            var tokens = await _db.RefreshTokens
                .Where(x => x.UserId == user.Id && x.Revoked == null && x.Expires > DateTime.UtcNow)
                .ToListAsync();
            foreach (var t in tokens)
            {
                t.Revoked = DateTime.UtcNow;
            }

            await _db.SaveChangesAsync();

            return Ok(ApiResponse<string>.Success(string.Empty, "Logged out successfully"));
        }
    }
}
