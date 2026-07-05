using Dars_5ososy_API.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Dars_5ososy_API.Infrastructure.Repositories
{
    public interface IAreaRepository
    {
        Task<List<Area>> GetAllAsync();
        Task<Area?> GetByEnglishNameAsync(string AreaEnglishName);
        Task<Area?> GetByArabicNameAsync(string AreaArabicName);
        Task<Area> CreateAsync(Area entity);
        Task<Area?> UpdateAsync(Area entity);
        Task<bool> DeleteByArabicNameAsync(string AreaArabicName);
        Task<bool> DeleteByEnglishNameAsync(string AreaEnglishName);
        Task<List<Area>> GetAllByProvinceEnglishNameAsync(string provinceEnglishName);
        Task<List<Area>> GetAllByProvinceArabicNameAsync(string provinceArabicName);
        Task<List<Area>> GetAllByGovernorateArabicNameAsync(string governorateArabicName);
        Task<List<Area>> GetAllByGovernorateEnglishNameAsync(string governorateEnglishName);
    }

    public class AreaRepository : IAreaRepository
    {
        public readonly AppDbContext _appDbContext;

        public AreaRepository(AppDbContext appDbContext)
        {
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
            return await _appDbContext.Areas
                .Where(a => a.Governorate.Province.EnglishName.ToUpper() == provinceEnglishName.ToUpper())
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Area>> GetAllByProvinceArabicNameAsync(string provinceArabicName)
        {
            return await _appDbContext.Areas
                .Where(a => a.Governorate.Province.ArabicName.ToUpper() == provinceArabicName.ToUpper())
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Area>> GetAllByGovernorateArabicNameAsync(string governorateArabicName)
        {
            return await _appDbContext.Areas
                .Where(a => a.Governorate.ArabicName.ToUpper() == governorateArabicName.ToUpper())
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Area>> GetAllByGovernorateEnglishNameAsync(string governorateEnglishName)
        {
            return await _appDbContext.Areas
                .Where(a => a.Governorate.EnglishName.ToUpper() == governorateEnglishName.ToUpper())
                .AsNoTracking()
                .ToListAsync();
        }
    }
}