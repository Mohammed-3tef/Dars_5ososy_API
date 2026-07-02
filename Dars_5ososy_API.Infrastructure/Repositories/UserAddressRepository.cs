using Dars_5ososy_API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dars_5ososy_API.Infrastructure.Repositories
{
    public class UserAddressRepository
    {
        private readonly AppDbContext _context;

        public UserAddressRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserAddress?> CreateAsync(UserAddress entity)
        {
            if (entity == null)
                return null;

            _context.UserAddresses.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(UserAddress userAddress)
        {
            var existingUserAddress = await _context.UserAddresses
                .FirstOrDefaultAsync(ua => ua.User.NormalizedUserName == userAddress.User.NormalizedUserName);

            if (existingUserAddress == null)
                return false;

            _context.UserAddresses.Remove(existingUserAddress);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<UserAddress>> GetAllAsync()
        {
            return await _context.UserAddresses.AsNoTracking().ToListAsync();
        }

        public async Task<UserAddress?> GetByUsernameAsync(string username)
        {
            return await _context.UserAddresses.AsNoTracking()
                .FirstOrDefaultAsync(ua => ua.User.NormalizedUserName == username.ToUpper());
        }

        public async Task<UserAddress?> UpdateAsync(UserAddress entity)
        {
            var existingEntity = await GetByUsernameAsync(entity.User.UserName);

            if (existingEntity == null)
                return null;

            existingEntity.Address = entity.Address;
            existingEntity.AreaId = entity.AreaId;

            _context.UserAddresses.Update(existingEntity);
            await _context.SaveChangesAsync();
            return existingEntity;
        }
    }
}
