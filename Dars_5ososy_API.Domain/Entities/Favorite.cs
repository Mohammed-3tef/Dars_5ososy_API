using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dars_5ososy_API.Domain.Entities
{
    public class Favorite
    {
        public long StudentId { get; set; }
        [ForeignKey(nameof(StudentId))]
        public User Student { get; set; }

        public long TeacherId { get; set; }
        [ForeignKey(nameof(TeacherId))]
        public User Teacher { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
