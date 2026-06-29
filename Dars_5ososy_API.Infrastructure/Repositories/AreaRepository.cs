using Dars_5ososy_API.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Dars_5ososy_API.Infrastructure.Repositories
{
    public class AreaRepository
    {
        private readonly UserManager<User> _userManager;
        public readonly AppDbContext _appDbContext;

        public AreaRepository(UserManager<User> userManager, AppDbContext appDbContext)
        {
            _userManager = userManager;
            _appDbContext = appDbContext;
        }

        public async Task<List<Area>> GetAllAsync()
        {
            return await _appDbContext.Areas.AsNoTracking().ToListAsync();
        }

        public async Task<Area?> GetByEnglishNameAsync(string AreaEnglishName)
        {
            return await _appDbContext.Areas.FirstOrDefaultAsync(s => s.EnglishName.ToUpper() == AreaEnglishName.ToUpper() || s.ArabicName.ToUpper() == AreaEnglishName.ToUpper());
        }

        public async Task<Area?> GetByArabicNameAsync(string AreaArabicName)
        {
            return await _appDbContext.Areas.FirstOrDefaultAsync(s => s.EnglishName.ToUpper() == AreaArabicName.ToUpper() || s.ArabicName.ToUpper() == AreaArabicName.ToUpper());
        }

        public async Task<Area> CreateAsync(Area entity)
        {
            var existsByAreaName = await GetByArabicNameAsync(entity.ArabicName);

            if (existsByAreaName != null)
                return null;

            await _appDbContext.Areas.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Area?> UpdateAsync(Area entity)
        {
            var existingArea = await GetByArabicNameAsync(entity.ArabicName);

            if (existingArea == null)
                return null;

            existingArea.EnglishName = entity.EnglishName;
            existingArea.ArabicName = entity.ArabicName;

            _appDbContext.Areas.Update(existingArea);
            await _appDbContext.SaveChangesAsync();
            return existingArea;
        }

        public async Task<bool> DeleteByArabicNameAsync(string AreaArabicName)
        {
            var Area = await GetByArabicNameAsync(AreaArabicName);

            if (Area == null)
                return false;

            _appDbContext.Areas.Remove(Area);
            var result = await _appDbContext.SaveChangesAsync();
            return result > 0;
        }
        
        public async Task<bool> DeleteByEnglishNameAsync(string AreaEnglishName)
        {
            var Area = await GetByEnglishNameAsync(AreaEnglishName);

            if (Area == null)
                return false;

            _appDbContext.Areas.Remove(Area);
            var result = await _appDbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<List<Area>> GetAllByProvinceEnglishNameAsync(string provinceEnglishName)
        {
            var province = await _appDbContext.Provinces
                .FirstOrDefaultAsync(p => p.EnglishName.ToUpper() == provinceEnglishName.ToUpper());
            return await _appDbContext.Areas
                .Where(a => a.Governorate.ProvinceId == province.Id).ToListAsync();
        }

        public async Task<List<Area>> GetAllByProvinceArabicNameAsync(string provinceArabicName)
        {
            var province = await _appDbContext.Provinces
                .FirstOrDefaultAsync(p => p.ArabicName.ToUpper() == provinceArabicName.ToUpper());
            return await _appDbContext.Areas
                .Where(a => a.Governorate.ProvinceId == province.Id).ToListAsync();
        }

        public async Task<List<Area>> GetAllByGovernorateArabicNameAsync(string governorateArabicName)
        {
            var governorate = await _appDbContext.Governorates
                .FirstOrDefaultAsync(g => g.ArabicName.ToUpper() == governorateArabicName.ToUpper());
            return await _appDbContext.Areas
                .Where(a => a.GovernorateId == governorate.Id).ToListAsync();
        }

        public async Task<List<Area>> GetAllByGovernorateEnglishNameAsync(string governorateEnglishName)
        {
            var governorate = await _appDbContext.Governorates
                .FirstOrDefaultAsync(g => g.EnglishName.ToUpper() == governorateEnglishName.ToUpper());
            return await _appDbContext.Areas
                .Where(a => a.GovernorateId == governorate.Id).ToListAsync();
        }
    }
}