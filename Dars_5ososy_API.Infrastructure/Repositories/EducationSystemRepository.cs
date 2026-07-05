using Dars_5ososy_API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dars_5ososy_API.Infrastructure.Repositories
{
    public interface IEducationSystemRepository : IGenericRepository<EducationSystem>
    {
        Task<EducationSystem?> GetByNameAsync(string EducationSystemName);
    }

    public class EducationSystemRepository : IEducationSystemRepository
    {
        public readonly AppDbContext _appDbContext;

        public EducationSystemRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<EducationSystem>> GetAllAsync()
        {
            return await _appDbContext.EducationSystems.AsNoTracking().ToListAsync();
        }

        public async Task<EducationSystem?> GetByIdAsync(long id)
        {
            return await _appDbContext.EducationSystems.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<EducationSystem?> GetByNameAsync(string EducationSystemName)
        {
            return await _appDbContext.EducationSystems.FirstOrDefaultAsync(s => s.EnglishName.ToUpper() == EducationSystemName.ToUpper() || s.ArabicName.ToUpper() == EducationSystemName.ToUpper());
        }

        public async Task<EducationSystem> CreateAsync(EducationSystem entity)
        {
            var existsByEducationSystemName = await GetByNameAsync(entity.EnglishName);

            if (existsByEducationSystemName != null)
                return null;

            await _appDbContext.EducationSystems.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<EducationSystem?> UpdateAsync(EducationSystem entity)
        {
            var existingEducationSystem = await GetByIdAsync(entity.Id);

            if (existingEducationSystem == null)
                return null;

            existingEducationSystem.EnglishName = entity.EnglishName;
            existingEducationSystem.ArabicName = entity.ArabicName;

            _appDbContext.EducationSystems.Update(existingEducationSystem);
            await _appDbContext.SaveChangesAsync();
            return existingEducationSystem;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var EducationSystem = await GetByIdAsync(id);

            if (EducationSystem == null)
                return false;

            _appDbContext.EducationSystems.Remove(EducationSystem);
            var result = await _appDbContext.SaveChangesAsync();
            return result > 0;
        }
    }
}