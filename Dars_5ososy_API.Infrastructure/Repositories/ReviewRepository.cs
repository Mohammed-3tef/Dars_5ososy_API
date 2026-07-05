using Dars_5ososy_API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dars_5ososy_API.Infrastructure.Repositories
{
    public interface IReviewRepository
    {
        Task<List<Review>> GetAllAsync();
        Task<Review> CreateAsync(Review entity);
        Task<Review?> UpdateAsync(Review entity);
        Task<bool> DeleteAsync(string studentUsername, string teacherUsername);
        Task<List<Review>> GetByTeacherUsernameAsync(string teacherUsername);
        Task<List<Review>> GetByStudentUsernameAsync(string studentUsername);
        Task<List<Review>> GetByStudentAndTeacherAsync(string studentUsername, string teacherUsername);
    }

    public class ReviewRepository : IReviewRepository
    {
        public readonly AppDbContext _appDbContext;

        public ReviewRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Review>> GetAllAsync()
        {
            return await _appDbContext.Reviews.AsNoTracking().ToListAsync();
        }

        public async Task<Review> CreateAsync(Review entity)
        {
            var existsByReviewName = await GetByStudentAndTeacherAsync(entity.Student.UserName, entity.Teacher.UserName);

            if (existsByReviewName != null && existsByReviewName.Any())
                return null;

            await _appDbContext.Reviews.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Review?> UpdateAsync(Review entity)
        {
            var existingReview = await GetByStudentAndTeacherAsync(entity.Student.UserName, entity.Teacher.UserName);

            if (existingReview == null || !existingReview.Any())
                return null;

            var reviewToUpdate = existingReview.First();
            reviewToUpdate.Rating = entity.Rating;
            reviewToUpdate.Comment = entity.Comment;

            _appDbContext.Reviews.Update(reviewToUpdate);
            await _appDbContext.SaveChangesAsync();
            return reviewToUpdate;
        }

        public async Task<bool> DeleteAsync(string studentUsername, string teacherUsername)
        {
            var Review = await GetByStudentAndTeacherAsync(studentUsername, teacherUsername);

            if (Review == null || !Review.Any())
                return false;

            _appDbContext.Reviews.RemoveRange(Review);
            var result = await _appDbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<List<Review>> GetByTeacherUsernameAsync(string teacherUsername)
        {
            return await _appDbContext.Reviews
                .Include(r => r.Teacher)
                .Where(r => r.Teacher.NormalizedUserName == teacherUsername.ToUpper())
                .ToListAsync();
        }

        public async Task<List<Review>> GetByStudentUsernameAsync(string studentUsername)
        {
            return await _appDbContext.Reviews
                .Include(r => r.Student)
                .Where(r => r.Student.NormalizedUserName == studentUsername.ToUpper())
                .ToListAsync();
        }

        public async Task<List<Review>> GetByStudentAndTeacherAsync(string studentUsername, string teacherUsername)
        {
            return await _appDbContext.Reviews
                .Include(r => r.Student)
                .Include(r => r.Teacher)
                .Where(r => r.Student.NormalizedUserName == studentUsername.ToUpper() && r.Teacher.NormalizedUserName == teacherUsername.ToUpper())
                .ToListAsync();
        }
    }
}