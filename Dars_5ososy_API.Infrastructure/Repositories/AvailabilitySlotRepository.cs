using Dars_5ososy_API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dars_5ososy_API.Infrastructure.Repositories
{
    public class AvailabilitySlotRepository
    {
        private readonly AppDbContext _context;

        public AvailabilitySlotRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AvailabilitySlot> CreateAsync(AvailabilitySlot entity)
        {
            var existingSlot = await _context.AvailabilitySlots
                .FirstOrDefaultAsync(a => a.TeacherId == entity.TeacherId &&
                                          a.DayOfWeek == entity.DayOfWeek &&
                                          a.StartTime == entity.StartTime &&
                                          a.EndTime == entity.EndTime);
            if (existingSlot != null)
                return existingSlot;
            
            await _context.AvailabilitySlots.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var entity = await _context.AvailabilitySlots.FindAsync(id);
            if (entity == null)
                return false;

            _context.AvailabilitySlots.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<AvailabilitySlot>> GetAllAsync()
        {
            return await _context.AvailabilitySlots.AsNoTracking().ToListAsync();
        }

        public async Task<List<AvailabilitySlot>> GetAllByUsernameAsync(string username)
        {
            return await _context.AvailabilitySlots
                .Include(a => a.Teacher)
                .Where(a => a.Teacher.NormalizedUserName == username.ToUpper())
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<AvailabilitySlot?> UpdateAsync(AvailabilitySlot entity)
        {
            var existingSlot = await _context.AvailabilitySlots
                .FirstOrDefaultAsync(a => a.TeacherId == entity.TeacherId &&
                                          a.DayOfWeek == entity.DayOfWeek &&
                                          a.StartTime == entity.StartTime &&
                                          a.EndTime == entity.EndTime);

            if (existingSlot != null && existingSlot.Id != entity.Id)
                return null;

            existingSlot.MaxStudents = entity.MaxStudents;
            existingSlot.Price = entity.Price;
            existingSlot.IsActive = entity.IsActive;
            existingSlot.Address = entity.Address;
            existingSlot.AreaId = entity.AreaId;
            existingSlot.EducationStageId = entity.EducationStageId;
            existingSlot.EducationSystemId = entity.EducationSystemId;
            existingSlot.StartTime = entity.StartTime;
            existingSlot.EndTime = entity.EndTime;
            existingSlot.DayOfWeek = entity.DayOfWeek;

            _context.AvailabilitySlots.Update(existingSlot);
            await _context.SaveChangesAsync();
            return existingSlot;
        }
    }
}
