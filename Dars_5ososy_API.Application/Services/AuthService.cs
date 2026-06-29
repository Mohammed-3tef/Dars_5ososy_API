using AutoMapper;
using Azure.Core;
using Dars_5ososy_API.Application.DTOs;
using Dars_5ososy_API.Application.DTOs.AuthDTOs;
using Dars_5ososy_API.Application.DTOs.UserDTOs;
using Dars_5ososy_API.Domain.Entities;
using Dars_5ososy_API.Infrastructure;
using Dars_5ososy_API.Shared.Helpers;
using Microsoft.AspNetCore.Identity;
using System;

namespace Dars_5ososy_API.Application.Services
{
    public class AuthService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly UserService _userService;
        private readonly TokenService _tokenService;
        private readonly EmailService _emailService;
        private readonly AppDbContext _db;

        public AuthService(IMapper mapper, UserManager<User> userManager, UserService userService, TokenService tokenService, EmailService emailService, AppDbContext db) {
            _mapper = mapper;
            _userManager = userManager;
            _userService = userService;
            _tokenService = tokenService;
            _emailService = emailService;
            _db = db;
        }

        public async Task<bool> RegisterUserAsync(CreatedUserDTO dto) {
            var existingEmail = await _userService.GetByEmailAsync(dto.Email);
            var existingUserName = await _userService.GetByUserNameAsync(dto.UserName);
            var existingPhoneNumber = await _userService.GetByPhoneNumberAsync(dto.PhoneNumber);

            if (existingEmail != null || existingUserName != null || existingPhoneNumber != null)
            {
                var errorMessages = new List<string>();
                if (existingEmail != null)
                    errorMessages.Add("Email already exists");
                if (existingUserName != null)
                    errorMessages.Add("Username already exists");
                if (existingPhoneNumber != null)
                    errorMessages.Add("Phone number already exists");
                var errorMessage = string.Join(", ", errorMessages);
                await ExceptionLogger.Log(new Exception($"{(dto.IsStudent ? "Student" : "Teacher")} registration failed: " + errorMessage));
                return false;
            }

            var user = _mapper.Map<User>(dto);
            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded) {
                await ExceptionLogger.Log(new Exception($"{(dto.IsStudent ? "Student" : "Teacher")} registration failed: " + string.Join(", ", result.Errors.Select(e => e.Description))));
                return false;
            }

            await _userManager.AddToRoleAsync(user, dto.IsStudent ? "Student" : "Teacher");
            return true;
        }

        public async Task<bool> CheckPasswordAsync(LoginDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null) return false;
            return await _userManager.CheckPasswordAsync(user, dto.Password);
        }

        public async Task<bool> ConfirmEmailAsync(UserDTO dto, string token)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded) return false;
            return true;
        }

        public async Task<string> GeneratePasswordResetTokenAsync(UserDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null) return null;
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<bool> ResetPasswordAsync(UserDTO dto, string token, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null) return false;

            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            if (!result.Succeeded) return false;

            return true;
        }
    }
}
