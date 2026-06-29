using Dars_5ososy_API.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Dars_5ososy_API.Infrastructure.Repositories
{
    public class ProvinceRepository
    {
        private readonly UserManager<User> _userManager;
        public readonly AppDbContext _appDbContext;

        public ProvinceRepository(UserManager<User> userManager, AppDbContext appDbContext)
        {
            _userManager = userManager;
            _appDbContext = appDbContext;
        }

        public async Task<List<Province>> GetAllAsync()
        {
            return await _appDbContext.Provinces.AsNoTracking().ToListAsync();
        }

        public async Task<Province?> GetByEnglishNameAsync(string ProvinceEnglishName)
        {
            return await _appDbContext.Provinces.FirstOrDefaultAsync(s => s.EnglishName.ToUpper() == ProvinceEnglishName.ToUpper() || s.ArabicName.ToUpper() == ProvinceEnglishName.ToUpper());
        }

        public async Task<Province?> GetByArabicNameAsync(string ProvinceArabicName)
        {
            return await _appDbContext.Provinces.FirstOrDefaultAsync(s => s.EnglishName.ToUpper() == ProvinceArabicName.ToUpper() || s.ArabicName.ToUpper() == ProvinceArabicName.ToUpper());
        }

        public async Task<Province> CreateAsync(Province entity)
        {
            var existsByProvinceName = await GetByArabicNameAsync(entity.ArabicName);

            if (existsByProvinceName != null)
                return null;

            await _appDbContext.Provinces.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Province?> UpdateAsync(Province entity)
        {
            var existingProvince = await GetByArabicNameAsync(entity.ArabicName);

            if (existingProvince == null)
                return null;

            existingProvince.EnglishName = entity.EnglishName;
            existingProvince.ArabicName = entity.ArabicName;

            _appDbContext.Provinces.Update(existingProvince);
            await _appDbContext.SaveChangesAsync();
            return existingProvince;
        }

        public async Task<bool> DeleteByArabicNameAsync(string ProvinceArabicName)
        {
            var Province = await GetByArabicNameAsync(ProvinceArabicName);

            if (Province == null)
                return false;

            _appDbContext.Provinces.Remove(Province);
            var result = await _appDbContext.SaveChangesAsync();
            return result > 0;
        }
        
        public async Task<bool> DeleteByEnglishNameAsync(string ProvinceEnglishName)
        {
            var Province = await GetByEnglishNameAsync(ProvinceEnglishName);

            if (Province == null)
                return false;

            _appDbContext.Provinces.Remove(Province);
            var result = await _appDbContext.SaveChangesAsync();
            return result > 0;
        }
    }
}