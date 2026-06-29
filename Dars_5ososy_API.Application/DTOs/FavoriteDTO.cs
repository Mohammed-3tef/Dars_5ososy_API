using Dars_5ososy_API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dars_5ososy_API.Application.DTOs
{
    public class FavoriteDTO
    {
        public string StudentUsername { get; set; }
        public string TeacherUsername { get; set; }
    }
}
