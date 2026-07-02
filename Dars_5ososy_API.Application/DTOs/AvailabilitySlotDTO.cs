using Dars_5ososy_API.Domain.Entities;

namespace Dars_5ososy_API.Application.DTOs
{
    public class AvailabilitySlotDTO
    {
        public string TeacherUsername { get; set; }

        public string DayOfWeek { get; set; }

        public string EducationStageArabicName { get; set; }
        public string EducationSystemArabicName { get; set; }

        public string Address { get; set; }
        public string Area { get; set; }

        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public bool IsActive { get; set; } = true;
        public int MaxStudents { get; set; }
        public decimal Price { get; set; }
    }
}
