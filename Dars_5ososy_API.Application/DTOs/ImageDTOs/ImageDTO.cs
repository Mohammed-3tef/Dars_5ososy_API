namespace Dars_5ososy_API.Application.DTOs.ImageDTOs
{
    public class ImageDTO
    {
        public int Id { get; set; }
        public byte[] Data { get; set; } = default!;
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public string Username { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}