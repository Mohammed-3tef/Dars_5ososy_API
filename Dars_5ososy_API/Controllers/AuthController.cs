using AutoMapper;
using Dars_5ososy_API.Application.DTOs;
using Dars_5ososy_API.Application.DTOs.AuthDTOs;
using Dars_5ososy_API.Application.DTOs.UserDTOs;
using Dars_5ososy_API.Application.Services;
using Dars_5ososy_API.Domain.Entities;
using Dars_5ososy_API.Infrastructure;
using Dars_5ososy_API.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Dars_5ososy_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
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

        [HttpPost("register")]
        public async Task<IActionResult> Register(CreatedUserDTO dto)
        {
            var result = await _authService.RegisterUserAsync(dto);

            if (!result)
                return BadRequest(ApiResponse<object>.Fail($"{(dto.IsStudent ? "Student" : "Teacher")} creation failed"));

            User user= _mapper.Map<User>(dto);
            var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var confirmationLink = Url.Action(
                "ConfirmEmail",
                "Auth",
                new { userId = user.Id, token = confirmationToken },
                Request.Scheme
            );

            string confirmEmailBody = "Please confirm your email address to activate your Dars 5ososy account and unlock all features.\n\nThis step is required to verify that this email belongs to you and to keep your account secure.\n\nClick the button below to confirm your email address.";

            EmailRequestDTO emailRequest = new EmailRequestDTO
            {
                ToEmail = user.Email,
                Subject = "Confirm your email",
                Body = EmailService.CreateEmailBody("Confirm your email", confirmEmailBody, confirmationLink),
            };
            await _emailService.SendEmailAsync(emailRequest);

            var token = await _tokenService.CreateToken(user);

            UserDTO userDTO = _mapper.Map<UserDTO>(user);
            ApiResponse<object> apiResponse = 
                ApiResponse<object>.Succeeded(new{token, user = userDTO}, "User registered successfully. Please check your email to confirm.");

            return Ok(apiResponse);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginDTO dto)
        {
            var user = await _userService.GetByEmailAsync(dto.Email);

            if (user == null)
                return Unauthorized(ApiResponse<object>.Fail("Invalid email or password"));

            var checkPassword = await _authService.CheckPasswordAsync(dto);

            if (!checkPassword)
                return Unauthorized(ApiResponse<object>.Fail("Invalid email or password"));

            var tokens = await _tokenService.CreateTokenPair(dto.Email);

            ApiResponse<object> apiResponse = ApiResponse<object>.Succeeded(new
            {
                token = tokens.AccessToken,
                refreshToken = tokens.RefreshToken,
                user
            }, "Login successful");

            return Ok(apiResponse);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(RefreshRequestDTO dto)
        {
            if (string.IsNullOrEmpty(dto?.RefreshToken))
                return BadRequest(ApiResponse<object>.Fail("Refresh token is required"));

            var existing = await _db.RefreshTokens
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Token == dto.RefreshToken && x.Revoked == null && x.Expires > DateTime.UtcNow);

            if (existing == null)
                return Unauthorized(ApiResponse<object>.Fail("Invalid refresh token"));

            // Revoke old token
            existing.Revoked = DateTime.UtcNow;
            await _db.SaveChangesAsync();

            var user = existing.User;
            var tokens = await _tokenService.CreateTokenPair(user.Email);

            return Ok(ApiResponse<object>.Succeeded(new
                {
                    token = tokens.AccessToken,
                    refreshToken = tokens.RefreshToken
                }, "Token refreshed successfully")
            );
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string email, string token)
        {
            var user = await _userService.GetByEmailAsync(email);

            if (user == null)
                return NotFound(ApiResponse<object>.Fail("User not found"));

            var result = await _authService.ConfirmEmailAsync(user, token);

            if (!result)
                return BadRequest(ApiResponse<object>.Fail("Invalid email confirmation"));

            return Ok(ApiResponse<string>.Succeeded(user.Email, "Email confirmed successfully"));
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var user = await _userService.GetByEmailAsync(email);

            if (user == null)
                return Ok(ApiResponse<object>.Succeeded(string.Empty, "If the email exists, a reset link has been sent.")); // don't expose users

            var token = await _authService.GeneratePasswordResetTokenAsync(user);

            var resetLink = Url.Action(
                "ResetPassword",
                "Auth",
                new { email, token },
                Request.Scheme
            );

            string resetEmailBody = "You're receiving this email because a request was made to reset your Dars 5ososy account password.\n\nFor your security, we need to verify this request before making any changes to your account.\n\nClick the button below to reset your password.";

            EmailRequestDTO emailRequest = new EmailRequestDTO
            {
                ToEmail = user.Email,
                Subject = "Reset your password",
                Body = EmailService.CreateEmailBody("Reset your password", resetEmailBody, resetLink),
            };
            await _emailService.SendEmailAsync(emailRequest);

            return Ok(ApiResponse<string>.Succeeded(string.Empty, "Reset link sent"));
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO dto)
        {
            var user = await _userService.GetByEmailAsync(dto.Email);

            if (user == null)
                return NotFound(ApiResponse<object>.Fail("User not found"));

            var decodedToken = WebUtility.UrlDecode(dto.Token);

            var result = await _authService
                .ResetPasswordAsync(user, decodedToken, dto.NewPassword);

            if (!result)
                return BadRequest(ApiResponse<object>.Fail("Password reset failed"));

            return Ok(ApiResponse<string>.Succeeded(string.Empty, "Password reset successful"));
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return Unauthorized(ApiResponse<object>.Fail("User not found"));

            // Revoke all active refresh tokens for this user
            var tokens = await _db.RefreshTokens
                .Where(x => x.UserId == user.Id && x.Revoked == null && x.Expires > DateTime.UtcNow)
                .ToListAsync();
            foreach (var t in tokens)
            {
                t.Revoked = DateTime.UtcNow;
            }

            await _db.SaveChangesAsync();

            return Ok(ApiResponse<string>.Succeeded(string.Empty, "Logged out successfully"));
        }
    }
}
