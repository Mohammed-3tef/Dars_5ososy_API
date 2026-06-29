using System.ComponentModel.DataAnnotations.Schema;

namespace Dars_5ososy_API.Domain.Entities
{
    public class Upload
    {
        public long Id { get; set; }

        public long UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        public Byte[] FileData { get; set; }
        public string FileType { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
