using Dars_5ososy_API.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Dars_5ososy_API.Infrastructure.Repositories
{
    public class UserRepository
    {
        private readonly UserManager<User> _userManager;

        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _userManager.Users.AsNoTracking().ToListAsync();
        }

        public async Task<User?> GetByIdAsync(long id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }

        public async Task<User?> GetByUserNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<User?> GetByPhoneNumberAsync(string phoneNumber)
        {
            return await _userManager.Users
                .FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
        }

        public async Task<List<string>?> GetRolesAsync(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            if (roles == null || !roles.Any())
                return null;

            return roles.ToList();
        }

        public async Task<User> CreateAsync(User entity, string password)
        {
            var existsByUserName = await GetByUserNameAsync(entity.UserName);
            var existsByEmail = await GetByEmailAsync(entity.Email);
            var existsByPhone = await GetByPhoneNumberAsync(entity.PhoneNumber);

            if (existsByUserName != null || existsByEmail != null || existsByPhone != null)
                return null;

            var result = await _userManager.CreateAsync(entity, password);

            if (!result.Succeeded)
                return null;

            return entity;
        }

        public async Task<User?> UpdateAsync(User entity)
        {
            var existingUser = await GetByIdAsync(entity.Id);

            if (existingUser == null)
                return null;

            // update fields instead of replacing blindly
            existingUser.UserName = entity.UserName;
            existingUser.FirstName = entity.FirstName;
            existingUser.LastName = entity.LastName;
            existingUser.Email = entity.Email;
            existingUser.PhoneNumber = entity.PhoneNumber;
            existingUser.PhotoUrl = entity.PhotoUrl;
            existingUser.Bio = entity.Bio;
            existingUser.BirthDate = entity.BirthDate;

            var result = await _userManager.UpdateAsync(existingUser);

            if (!result.Succeeded)
                return null;

            return existingUser;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var user = await GetByIdAsync(id);

            if (user == null)
                return false;

            var result = await _userManager.DeleteAsync(user);

            return result.Succeeded;
        }
    }
}