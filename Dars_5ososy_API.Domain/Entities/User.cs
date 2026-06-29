using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Dars_5ososy_API.Domain.Entities
{
    public class User : IdentityUser<long>
    {
        [Required]
        public long NationalId { get; set; }
        
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Birth Date")]
        public DateOnly BirthDate { get; set; }
        public string? PhotoUrl { get; set; }
        public string? Bio { get; set; }
        public char Gender { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
