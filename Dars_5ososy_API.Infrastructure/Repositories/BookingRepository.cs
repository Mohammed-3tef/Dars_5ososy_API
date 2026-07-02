using Dars_5ososy_API.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Dars_5ososy_API.Infrastructure.Repositories
{
    public class BookingRepository
    {
        private readonly UserManager<User> _userManager;
        public readonly AppDbContext _appDbContext;

        public BookingRepository(UserManager<User> userManager, AppDbContext appDbContext)
        {
            _userManager = userManager;
            _appDbContext = appDbContext;
        }

        public async Task<List<Booking>> GetAllAsync()
        {
            return await _appDbContext.Bookings.AsNoTracking().ToListAsync();
        }

        public async Task<Booking> CreateAsync(Booking entity)
        {
            var existsByBookingName = await GetByStudentAndTeacherAsync(entity.Student.UserName, entity.Teacher.UserName);

            if (existsByBookingName != null && existsByBookingName.Any())
                return null;

            await _appDbContext.Bookings.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(string studentUsername, string teacherUsername)
        {
            var Booking = await GetByStudentAndTeacherAsync(studentUsername, teacherUsername);

            if (Booking == null || !Booking.Any())
                return false;

            _appDbContext.Bookings.RemoveRange(Booking);
            var result = await _appDbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<List<Booking>> GetByTeacherUsernameAsync(string teacherUsername)
        {
            return await _appDbContext.Bookings
                .Include(r => r.Teacher)
                .Where(r => r.Teacher.NormalizedUserName == teacherUsername.ToUpper())
                .ToListAsync();
        }

        public async Task<List<Booking>> GetByStudentUsernameAsync(string studentUsername)
        {
            return await _appDbContext.Bookings
                .Include(r => r.Student)
                .Where(r => r.Student.NormalizedUserName == studentUsername.ToUpper())
                .ToListAsync();
        }

        public async Task<List<Booking>> GetByStudentAndTeacherAsync(string studentUsername, string teacherUsername)
        {
            return await _appDbContext.Bookings
                .Include(r => r.Student)
                .Include(r => r.Teacher)
                .Where(r => r.Student.NormalizedUserName == studentUsername.ToUpper() && r.Teacher.NormalizedUserName == teacherUsername.ToUpper())
                .ToListAsync();
        }
    }
}