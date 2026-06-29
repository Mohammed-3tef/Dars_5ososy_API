using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dars_5ososy_API.Application.DTOs.UserDTOs
{
    public class UserDTO
    {
        public long NationalId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Gender { get; set; }
        public string? PhotoUrl { get; set; }
        public string? Bio { get; set; }
    }
}
