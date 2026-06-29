using Dars_5ososy_API.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Dars_5ososy_API.Infrastructure.Repositories
{
    public class SubjectRepository : IGenericRepository<Subject>
    {
        private readonly UserManager<User> _userManager;
        public readonly AppDbContext _appDbContext;

        public SubjectRepository(UserManager<User> userManager, AppDbContext appDbContext)
        {
            _userManager = userManager;
            _appDbContext = appDbContext;
        }

        public async Task<List<Subject>> GetAllAsync()
        {
            return await _appDbContext.Subjects.AsNoTracking().ToListAsync();
        }

        public async Task<Subject?> GetByIdAsync(long id)
        {
            return await _appDbContext.Subjects.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Subject?> GetByNameAsync(string subjectName)
        {
            return await _appDbContext.Subjects.FirstOrDefaultAsync(s => s.Name.ToUpper() == subjectName.ToUpper());
        }

        public async Task<Subject> CreateAsync(Subject entity)
        {
            var existsBySubjectName = await GetByNameAsync(entity.Name);

            if (existsBySubjectName != null)
                return null;

            await _appDbContext.Subjects.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Subject?> UpdateAsync(Subject entity)
        {
            var existingSubject = await GetByIdAsync(entity.Id);

            if (existingSubject == null)
                return null;

            existingSubject.Description = entity.Description;
            existingSubject.UpdatedAt = DateTime.UtcNow;

            _appDbContext.Subjects.Update(existingSubject);
            await _appDbContext.SaveChangesAsync();
            return existingSubject;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var Subject = await GetByIdAsync(id);

            if (Subject == null)
                return false;

            _appDbContext.Subjects.Remove(Subject);
            var result = await _appDbContext.SaveChangesAsync();
            return result > 0;
        }
    }
}