using Microsoft.AspNetCore.Http;

namespace Dars_5ososy_API.Application.DTOs
{
    public class UploadImageDTO
    {
        public IFormFile Image { get; set; } = default!;
    }
}
