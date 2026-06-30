using System.ComponentModel.DataAnnotations.Schema;

namespace Dars_5ososy_API.Domain.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public byte[] Data { get; set; } = default!;
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public long FileSize { get; set; }

        public long UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
