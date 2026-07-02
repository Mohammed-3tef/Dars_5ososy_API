using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Dars_5ososy_API.Application.DTOs.ImageDTOs
{
    public class UpdateImageDTO
    {
        [Required]
        public IFormFile Image { get; set; } = default!;
    }
}