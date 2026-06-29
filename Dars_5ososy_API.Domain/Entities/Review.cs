using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dars_5ososy_API.Domain.Entities
{
    public class Review
    {
        public long Id { get; set; }

        [Range(0.5, 5)]
        public decimal Rating { get; set; }

        public string? Comment { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

        public long StudentId { get; set; }
        [ForeignKey(nameof(StudentId))]
        public User Student { get; set; }

        public long TeacherId { get; set; }
        [ForeignKey(nameof(TeacherId))]
        public User Teacher { get; set; }
    }
}
