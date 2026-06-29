using System.ComponentModel.DataAnnotations.Schema;

namespace Dars_5ososy_API.Domain.Entities
{
    public class AvailabilitySlot
    {
        public long Id { get; set; }
        public long TeacherId { get; set; }
        [ForeignKey(nameof(TeacherId))]
        public User Teacher { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        public int EducationStageId { get; set; }
        [ForeignKey(nameof(EducationStageId))]
        public EducationStage EducationStage { get; set; }

        public int EducationSystemId { get; set; }
        [ForeignKey(nameof(EducationSystemId))]
        public EducationSystem EducationSystem { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsActive { get; set; } = true;
        public int MaxStudents { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
