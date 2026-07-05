using Dars_5ososy_API.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Dars_5ososy_API.Infrastructure.Repositories
{
    public interface IFavoriteRepository
    {
        Task<List<Favorite>> GetAllAsync();
        Task<Favorite> CreateAsync(Favorite entity);
        Task<bool> DeleteAsync(string studentUsername, string teacherUsername);
        Task<List<Favorite>> GetByTeacherUsernameAsync(string teacherUsername);
        Task<List<Favorite>> GetByStudentUsernameAsync(string studentUsername);
        Task<List<Favorite>> GetByStudentAndTeacherAsync(string studentUsername, string teacherUsername);
    }

    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly UserManager<User> _userManager;
        public readonly AppDbContext _appDbContext;

        public FavoriteRepository(UserManager<User> userManager, AppDbContext appDbContext)
        {
            _userManager = userManager;
            _appDbContext = appDbContext;
        }

        public async Task<List<Favorite>> GetAllAsync()
        {
            return await _appDbContext.Favorites.AsNoTracking().ToListAsync();
        }

        public async Task<Favorite> CreateAsync(Favorite entity)
        {
            var existsByFavoriteName = await _appDbContext.Favorites
                .Include(f => f.Student)
                .Include(f => f.Teacher)
                .FirstOrDefaultAsync(f => f.Student.NormalizedUserName == entity.Student.NormalizedUserName && f.Teacher.NormalizedUserName == entity.Teacher.NormalizedUserName);

            if (existsByFavoriteName != null)
                return null;

            await _appDbContext.Favorites.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(string studentUsername, string teacherUsername)
        {
            var Favorite = await _appDbContext.Favorites
                .Include(f => f.Student)
                .Include(f => f.Teacher)
                .FirstOrDefaultAsync(f => f.Student.NormalizedUserName == studentUsername.ToUpper() && f.Teacher.NormalizedUserName == teacherUsername.ToUpper());

            if (Favorite == null)
                return false;

            _appDbContext.Favorites.Remove(Favorite);
            var result = await _appDbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<List<Favorite>> GetByTeacherUsernameAsync(string teacherUsername)
        {
            return await _appDbContext.Favorites
                .Include(r => r.Teacher)
                .Where(r => r.Teacher.NormalizedUserName == teacherUsername.ToUpper())
                .ToListAsync();
        }

        public async Task<List<Favorite>> GetByStudentUsernameAsync(string studentUsername)
        {
            return await _appDbContext.Favorites
                .Include(r => r.Student)
                .Where(r => r.Student.NormalizedUserName == studentUsername.ToUpper())
                .ToListAsync();
        }

        public async Task<List<Favorite>> GetByStudentAndTeacherAsync(string studentUsername, string teacherUsername)
        {
            return await _appDbContext.Favorites
                .Include(r => r.Student)
                .Include(r => r.Teacher)
                .Where(r => r.Student.NormalizedUserName == studentUsername.ToUpper() && r.Teacher.NormalizedUserName == teacherUsername.ToUpper())
                .ToListAsync();
        }
    }
}