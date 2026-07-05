using Dars_5ososy_API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dars_5ososy_API.Infrastructure.Repositories
{
    public interface IEducationStageRepository : IGenericRepository<EducationStage>
    {
        Task<EducationStage?> GetByNameAsync(string EducationStageName);
    }

    public class EducationStageRepository : IEducationStageRepository
    {
        public readonly AppDbContext _appDbContext;

        public EducationStageRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<EducationStage>> GetAllAsync()
        {
            return await _appDbContext.EducationStages.AsNoTracking().ToListAsync();
        }

        public async Task<EducationStage?> GetByIdAsync(long id)
        {
            return await _appDbContext.EducationStages.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<EducationStage?> GetByNameAsync(string EducationStageName)
        {
            return await _appDbContext.EducationStages.FirstOrDefaultAsync(s => s.EnglishName.ToUpper() == EducationStageName.ToUpper() || s.ArabicName.ToUpper() == EducationStageName.ToUpper());
        }

        public async Task<EducationStage> CreateAsync(EducationStage entity)
        {
            var existsByEducationStageName = await GetByNameAsync(entity.EnglishName);

            if (existsByEducationStageName != null)
                return null;

            await _appDbContext.EducationStages.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<EducationStage?> UpdateAsync(EducationStage entity)
        {
            var existingEducationStage = await GetByIdAsync(entity.Id);

            if (existingEducationStage == null)
                return null;

            existingEducationStage.EnglishName = entity.EnglishName;
            existingEducationStage.ArabicName = entity.ArabicName;

            _appDbContext.EducationStages.Update(existingEducationStage);
            await _appDbContext.SaveChangesAsync();
            return existingEducationStage;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var EducationStage = await GetByIdAsync(id);

            if (EducationStage == null)
                return false;

            _appDbContext.EducationStages.Remove(EducationStage);
            var result = await _appDbContext.SaveChangesAsync();
            return result > 0;
        }
    }
}