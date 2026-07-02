using System.ComponentModel.DataAnnotations.Schema;

namespace Dars_5ososy_API.Domain.Entities
{
    public class Booking
    {
        public long StudentId { get; set; }
        [ForeignKey(nameof(StudentId))]
        public User Student { get; set; }

        public long TeacherId { get; set; }
        [ForeignKey(nameof(TeacherId))]
        public User Teacher { get; set; }

        public long AvailabilitySlotId { get; set; }
        [ForeignKey(nameof(AvailabilitySlotId))]
        public AvailabilitySlot AvailabilitySlot { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
