using System.ComponentModel.DataAnnotations.Schema;

namespace Dars_5ososy_API.Domain.Entities
{
    public class TeacherSubject
    {
        public long TeacherId { get; set; }
        [ForeignKey(nameof(TeacherId))]
        public User Teacher { get; set; }

        public long SubjectId { get; set; }
        [ForeignKey(nameof(SubjectId))]
        public Subject Subject { get; set; }
    }
}
