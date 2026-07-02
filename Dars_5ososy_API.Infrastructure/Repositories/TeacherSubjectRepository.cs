using Dars_5ososy_API.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Dars_5ososy_API.Infrastructure.Repositories
{
    public class TeacherSubjectRepository
    {
        private readonly UserManager<User> _userManager;
        public readonly AppDbContext _appDbContext;

        public TeacherSubjectRepository(UserManager<User> userManager, AppDbContext appDbContext)
        {
            _userManager = userManager;
            _appDbContext = appDbContext;
        }

        public async Task<List<TeacherSubject>> GetAllAsync()
        {
            return await _appDbContext.TeacherSubjects.AsNoTracking().ToListAsync();
        }

        public async Task<TeacherSubject> CreateAsync(TeacherSubject entity)
        {
            var existsByTeacherSubjectName = await GetByStudentAndTeacherAsync(entity.Subject.Code, entity.Teacher.UserName);

            if (existsByTeacherSubjectName != null && existsByTeacherSubjectName.Any())
                return null;

            await _appDbContext.TeacherSubjects.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(string studentUsername, string teacherUsername)
        {
            var TeacherSubject = await GetByStudentAndTeacherAsync(studentUsername, teacherUsername);

            if (TeacherSubject == null || !TeacherSubject.Any())
                return false;

            _appDbContext.TeacherSubjects.RemoveRange(TeacherSubject);
            var result = await _appDbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<List<TeacherSubject>> GetByTeacherUsernameAsync(string teacherUsername)
        {
            return await _appDbContext.TeacherSubjects
                .Include(r => r.Teacher)
                .Where(r => r.Teacher.NormalizedUserName == teacherUsername.ToUpper())
                .ToListAsync();
        }

        public async Task<List<TeacherSubject>> GetBySubjectCodeAsync(string subjectCode)
        {
            return await _appDbContext.TeacherSubjects
                .Include(r => r.Subject)
                .Where(r => r.Subject.Code.ToUpper() == subjectCode.ToUpper())
                .ToListAsync();
        }

        public async Task<List<TeacherSubject>> GetByStudentAndTeacherAsync(string subjectCode, string teacherUsername)
        {
            return await _appDbContext.TeacherSubjects
                .Include(r => r.Subject)
                .Include(r => r.Teacher)
                .Where(r => r.Subject.Code.ToUpper() == subjectCode.ToUpper() && r.Teacher.NormalizedUserName == teacherUsername.ToUpper())
                .ToListAsync();
        }
    }
}