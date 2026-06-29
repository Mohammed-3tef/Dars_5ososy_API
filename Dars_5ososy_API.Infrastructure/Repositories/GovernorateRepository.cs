using Dars_5ososy_API.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Dars_5ososy_API.Infrastructure.Repositories
{
    public class GovernorateRepository
    {
        private readonly UserManager<User> _userManager;
        public readonly AppDbContext _appDbContext;

        public GovernorateRepository(UserManager<User> userManager, AppDbContext appDbContext)
        {
            _userManager = userManager;
            _appDbContext = appDbContext;
        }

        public async Task<List<Governorate>> GetAllAsync()
        {
            return await _appDbContext.Governorates.AsNoTracking().ToListAsync();
        }

        public async Task<Governorate?> GetByEnglishNameAsync(string GovernorateEnglishName)
        {
            return await _appDbContext.Governorates.FirstOrDefaultAsync(s => s.EnglishName.ToUpper() == GovernorateEnglishName.ToUpper() || s.ArabicName.ToUpper() == GovernorateEnglishName.ToUpper());
        }

        public async Task<Governorate?> GetByArabicNameAsync(string GovernorateArabicName)
        {
            return await _appDbContext.Governorates.FirstOrDefaultAsync(s => s.EnglishName.ToUpper() == GovernorateArabicName.ToUpper() || s.ArabicName.ToUpper() == GovernorateArabicName.ToUpper());
        }

        public async Task<Governorate> CreateAsync(Governorate entity)
        {
            var existsByGovernorateName = await GetByArabicNameAsync(entity.ArabicName);

            if (existsByGovernorateName != null)
                return null;

            await _appDbContext.Governorates.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Governorate?> UpdateAsync(Governorate entity)
        {
            var existingGovernorate = await GetByArabicNameAsync(entity.ArabicName);

            if (existingGovernorate == null)
                return null;

            existingGovernorate.EnglishName = entity.EnglishName;
            existingGovernorate.ArabicName = entity.ArabicName;

            _appDbContext.Governorates.Update(existingGovernorate);
            await _appDbContext.SaveChangesAsync();
            return existingGovernorate;
        }

        public async Task<bool> DeleteByArabicNameAsync(string GovernorateArabicName)
        {
            var Governorate = await GetByArabicNameAsync(GovernorateArabicName);

            if (Governorate == null)
                return false;

            _appDbContext.Governorates.Remove(Governorate);
            var result = await _appDbContext.SaveChangesAsync();
            return result > 0;
        }
        
        public async Task<bool> DeleteByEnglishNameAsync(string GovernorateEnglishName)
        {
            var Governorate = await GetByEnglishNameAsync(GovernorateEnglishName);

            if (Governorate == null)
                return false;

            _appDbContext.Governorates.Remove(Governorate);
            var result = await _appDbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<List<Governorate>> GetAllByProvinceArabicNameAsync(string provinceArabicName)
        {
            return await _appDbContext.Governorates
                .Where(g => g.Province.ArabicName.ToUpper() == provinceArabicName.ToUpper())
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Governorate>> GetAllByProvinceEnglishNameAsync(string provinceEnglishName)
        {
            return await _appDbContext.Governorates
                .Where(g => g.Province.EnglishName.ToUpper() == provinceEnglishName.ToUpper())
                .AsNoTracking()
                .ToListAsync();
        }
    }
}